import React from 'react';
import AdminHeader from '../AdminDashboard/AdminHeader/AdminHeader';
import Back from './Back/Back';
import Form from './Form/Form';
import DocumentTable from './DocumentTable/DocumentTable';
import { useState, useEffect } from 'react';
import StartUpDefaultsValue from '../../ApiServices/StartUpLoadData';
import axios from 'axios';
function TrainingDocuments(props) {
  const [fileItems, setFileItems] = useState("");
  const [fullfileData, setFullFileData] = useState([]);
  let fullData;
  let recordsFromServer = StartUpDefaultsValue().then((response) => {
    fullData = response.data;
    fullData.forEach((item) => {
      let temp = item.FileContent.split('\\');
      item.FileContent = 'http://localhost:5000/files/' + temp[7];
    });
   // setFileItems(fullData);
     
    // setFullFileData(fullData);
  });
  console.log(fullData);
  // setFileItems(fullData);
  const [filter, setFilter] = useState({
    company: 'ALL',
    version: '',
    training: 'ALL',
  });

  useEffect(() => {
    StartUpDefaultsValue();
    return () => {
      setFileItems([]);
      // eslint-disable-next-line react-hooks/exhaustive-deps
    };
  }, []);

  useEffect(() => {
    filterTheLists();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [filter]);

  function filterByID(item) {
    let { company, version, training } = filter;
    if (
      (item.Company === company || company === 'ALL') &&
      (item.Version === version || version === '') &&
      (item.Training === training || training === 'ALL')
    ) {
      return true;
    }
    return false;
  }

  let arrFiltered = [];

  function filterTheLists() {
    arrFiltered = fullfileData.filter(filterByID);
    setFileItems(arrFiltered);
  }
  return (
    <div className='training'>
      <Back page='/trainingdocuments' />
      <AdminHeader
        style={{ marginTop: '1rem', padding: '1rem', borderTopColor: 'white' }}
        className='doc-header'
        title='Training Documents'
      />
      <Form
        item={props}
        fileItems={fileItems}
        setFileItems={setFileItems}
        fullfileData={fullfileData}
        setFilter={setFilter}
      />
      <DocumentTable
        fileItems={fileItems}
        setFileItems={setFileItems}
        fullData={fullData}
        onDeleted={() => {
          // StartUpDefaultsValue();
        }}
      />
    </div>
  );
}

export default TrainingDocuments;
