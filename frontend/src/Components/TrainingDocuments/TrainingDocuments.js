/* eslint-disable react-hooks/exhaustive-deps */
import AdminHeader from "../AdminDashboard/AdminHeader/AdminHeader";
import Back from "./Back/Back";
import Form from "./Form/Form";
import DocumentTable from "./DocumentTable/DocumentTable";
import { useState, useEffect } from "react";
import StartUpDefaultsValue from "../../ApiServices/StartUpLoadData";
import axios from "axios";

const Index = (props) => {
  const [fileItems, setFileItems] = useState("");
  const [fullfileData, setFullFileData] = useState();
  const [filter, setFilter] = useState({company: "ALL", version: "", training: "ALL"});

  let fullData;

  const fetchData = () => {
     StartUpDefaultsValue().then((response) => {
       fullData = response.data;
      //  console.log(fullData);
      //  setFullFileData(fullData)

       fullData.forEach((item) => {
         let temp = item.FileContent.split("\\");
         item.FileContent = "http://localhost:5000/files/" + temp[7];
       });

       setFullFileData(fullData);
     });
  }
 


  useEffect(() => {
    fetchData();
  },[]);

  // useEffect(() => {
  //   filterTheLists();
  // }, [filter]);

  // const filterByID = (item) => {
  //   let { company, version, training } = filter;
  //   if (
  //     (item.Company === company || company === "ALL") &&
  //     (item.Version === version || version === "") &&
  //     (item.Training === training || training === "ALL")
  //   ) {
  //     return true;
  //   }
  //   return false;
  // };

  // let arrFiltered = [];

  // const filterTheLists = () => {
  //   arrFiltered = fullfileData.filter(filterByID);
  //   setFileItems(arrFiltered);
  // };

  return (
    <div className="training">
      <Back page="/trainingdocuments" />
      <AdminHeader
        style={{ marginTop: "1rem", padding: "1rem", borderTopColor: "white" }}
        className="doc-header"
        title="Training Documents"
      />
     <Form
        item={props}
        fullfileData={fullfileData}
      />  
    
       <DocumentTable 
        fullData={fullfileData}
        onDeleted={() => {
          StartUpDefaultsValue();
        }}
      /> 
    </div>
  );
};

export default Index;