import DataTable from 'react-data-table-component';
import pdf from '../../../assets/pdf.png';
import edit from '../../../assets/edit.png';
import deleted from '../../../assets/deleted.png';
import { useHistory } from 'react-router';
import './DocumentTable.css';
import {useState,useEffect} from 'react';

const paginationComponentOptions = {
    rowsPerPageText: 'No of Rows per page',
    rangeSeparatorText: 'of',
    selectAllRowsItem: true,
  };

function DocumentTable(props) {
   const [data,setData]=useState();
    const history = useHistory();
    const columns = [
     
      {
        name: 'Name',
        selector: (row) => row.FileName,
        sortable: true,
      },
      {
        name: 'Training',
        selector: (row) => row.Training,
        sortable: true,
      },
      {
        name: 'Version',
        selector: (row) => row.Version,
        sortable: true,
      },
      {
        name: 'Company',
        selector: (row) => row.Company,
        sortable: true,
      },
      {
        name: 'Action',
        sortable: true,
        cell: (row) => (
          <div>
            <a
              href={row.FileContent}
              id={row.ID}
              target='_blank'
              rel='noreferrer'
            >
              <img src={pdf} alt='' />
            </a>
  
            <button
              style={{ backgroundColor: 'white' }}
              className='upload-button'
              onClick={() => {
                history.push('/uploaddocument', {
                  Company: row.Company,
                  Version: row.Version,
                  Training: row.Training,
                  FileName: row.FileName,
                  isNotEditable: true,
                });
              }}
            >
              {' '}
              <img src={edit} alt='' />
            </button>
  
            <button
              style={{ border: 'none' }}
              onClick={() => deleteFile(row.FileName, row.Version, row.Company)}
            >
              <img src={deleted} alt='' />
            </button>
          </div>
        ),
        ignoreRowClick: true,
        allowOverflow: true,
        button: true,
      },
    ];
    function deleteFile(fileName, fileVersion, fileCompany) {
      var userPreference;
      console.log("filename is:",fileName);
    //  console.log("id is:",ID);
      // eslint-disable-next-line no-restricted-globals
      // if (confirm('Deleted File Cant be Restored?') === true) {
      //   userPreference = 'Data saved successfully!';
  
      //   var xhttp = new XMLHttpRequest();
      //   xhttp.onreadystatechange = function () {
      //     if (this.readyState === 4 && this.status === 200) {
      //       props.onDeleted();
      //       console.log(props);
      //     }
      //   };
      //   xhttp.open(
      //     'GET',
      //     'http://localhost:5000/api/task/filedelete?file=' +
      //       fileName +
      //       '|' +
      //       fileVersion +
      //       '|' +
      //       fileCompany,
      //     true
      //   );
      //   xhttp.send();
      // } else {
      //   userPreference = 'Save Cancelled!';
      // }
    }

    const handleData=()=>{
  

      setData(props.fullData);
      console.log("data is:",data);
    }

useEffect(() => {
handleData();
  
}, [handleData])

  return (
    <div className='datatable-items'>
      <DataTable
        handle
        columns={columns}
        data={data}
        pagination
        paginationComponentOptions={paginationComponentOptions}
      />
    </div>
  );
}

export default DocumentTable;
