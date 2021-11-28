import React, { useRef ,useState} from 'react';
import { Link } from 'react-router-dom';
import './Form.css';
import filter from '../../../assets/filter.png';
import upload from '../../../assets/upload.png';
import {useHistory} from 'react-router-dom';
import FilterData from '../../../ApiServices/FilterData';

function Form(props) {
  const history=useHistory();
  console.log(props.fullfileData);
  const dataForm = useRef(null);
  const [company,setCompany]=useState("ALL");
  const [version,setVersion]=useState();
  const [training,setTraining]=useState("ALL");
 

  function handleChange() {
    console.log(props.item);
    props.item.setTraining(training);
    props.item.setCompany(company);
    props.item.setVersion(version);
    history.push('/uploaddocument', {
      fullFileItem: props.fileItems,
    }
 );
  }
  function handleClick(e) {
    e.preventDefault();
    console.log(company);
    console.log(version);
    console.log(training);
   FilterData(company,version,training);
    
  }
  return (
    <form ref={dataForm}>
      <div className='data-form'>
        <div>
          <div>Company</div>
        </div>
        <div>
          <div>Version</div>
        </div>
        <div>
          <div>Training</div>
        </div>
        <div>
          <div></div>
        </div>
        <div>
          <div></div>
        </div>
        <div>
          <div>
            <select onChange={(e)=>{setCompany(e.target.value)}} id='company'>
              <option value='ALL' selected={(company=='ALL')} >ALL</option>
              <option value='HPCL' selected={(company=='HPCL')}>HPCL</option>
              <option value='IOCL' selected={(company=='IOCL')}>IOCL</option>
              <option value='BPL'selected={(company=='BPL')} >BPL</option>
              <option value='GRK'selected={(company=='GRK')}>GRK</option>
            </select>
          </div>
        </div>
        <div>
          <div>
            <input type='text' id='version' onChange={(e)=>{setVersion(e.target.value)}} />
          </div>
        </div>
        <div>
          <div>
            <select id='training' onChange={(e)=>{setTraining(e.target.value)}}>
              <option value='ALL' selected={(training=='ALL')} >ALL</option>
              <option value='file management' selected={(training=='file management')} >file management</option>
              <option value='process management' selected={(training=='process management')} >process management</option>
              <option value='security management' selected={(training=='security management')} >security management</option>
              <option value='device management' selected={(training=='device management')} >device management</option>
            </select>
          </div>
        </div>
        <div>
          <div>
            <button
              className='filter-button'
              onClick={handleClick}
            >
              <img src={filter} alt='' />
              <span>Filter</span>
            </button>
          </div>
        </div>
        <div>
          <div>
         <button  className='upload-button' onClick={()=>{ handleChange()}} > <img src={upload} alt='' />
              <span>Upload</span></button>
            {/* <Link
              to='/uploaddocument'
              className='upload-button'
              onClick={() => {
                handleChange();
                history.push('/uploaddocument',
                 {state:{ fileItems:props.fileItems}},
              );
              }}
            >
              <img src={upload} alt='' />
              <span>Upload</span>
            </Link> */}
          </div>
        </div>
      </div>
    </form>
  );
}

export default Form;
