import React, { useState, useEffect } from 'react';
import Back from '../TrainingDocuments/Back/Back';
import { useLocation } from 'react-router-dom';
import './UploadFile.css';

function UploadFile(props) {
  const [file, setFile] = useState(null);
  const [fileName, setFileName] = useState(null);
  const location = useLocation();
  let mode = '';
  function SaveFileToServer(
    fileToLoad,
    fileName,
    company,
    training,
    version,
    minVersion
  ) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
      if (this.readyState === 4 && this.status === 200) {
      }
    };
    xhttp.open('PUT', 'http://localhost:5000/api/task/filesave', true);
    xhttp.setRequestHeader('Content-Type', 'application/json');
    xhttp.send(
      JSON.stringify({
        fileName: fileName,
        fileContent: fileToLoad,
        Company: company,
        Training: training,
        Version: version,
        MinVersion: minVersion,
        Mode: mode,
      })
    );
  }

  const fileContent = () => {
    if (
      document.getElementById('upload-file').files < 1 ||
      !document.getElementById('upload-file').validity.valid
    ) {
      return;
    }
    fileToBase64(
      document.getElementById('upload-file').files[0],
      (err, result) => {
        if (result) {
          setFile(result);
          setFileName(document.getElementById('upload-file').files[0]);
          let version = document.querySelector('#version').value;
          let company = document.querySelector('#company').value;
          let training = document.querySelector('#training').value;
          let fileName = document.getElementById('upload-file').files[0].name;
          let reqminVersion = document.getElementById('reqmin').value;
          console.log(result);
          SaveFileToServer(
            result,
            fileName,
            company,
            training,
            version,
            reqminVersion
          );
        }
      }
    );
  };

  const fileToBase64 = (file, cb) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      cb(null, reader.result);
    };
    reader.onerror = function (error) {
      cb(error, null);
    };
  };

  const uploadFiletoServer = () => {
    let version = document.querySelector('#version').value;
    let company = document.querySelector('#company').value;
    let training = document.querySelector('#training').value;
    console.log(document.getElementById('upload-file').files.length);
    var temp = document.getElementById('upload-file').files.length;

    let reqminVersion = document.getElementById('reqmin').value;
    if (temp <= 0) {
      alert('Please Upload a file');
    } else {
      //content of file include
      if (reqminVersion <= version && temp > 0) {
        let fileName = document.getElementById('upload-file').files[0].name;
        if (location.state.fullFileItem) {
          console.log(location.state.fullFileItem[0]);
          var arrayItems = location.state.fullFileItem;
          let flag = true;
          arrayItems.forEach(function (item) {
            console.log(item['FileName']);
            if (
              item['FileName'] === fileName &&
              item['Company'] === company &&
              item['Version'] === version &&
              item['Training'] === training
            ) {
              alert('File already exists');
              flag = false;
            }
          });
          if (flag === true) {
            alert('File Uploaded');
            mode = 'upload';
            fileContent();
          }
        } else {
          console.log('hello-edit');
          console.log(location.state.FileName, fileName);
          if (location.state.FileName === fileName) {
            alert('File Uploaded');
            fileContent();
            mode = 'edit';
          } else {
            alert('File Name should be same');
          }
        }
      } else {
        alert('minimum version should be atmost version');
      }
    }
  };

  return (
    <div>
      <div className='back-arrow'>
        <Back page='/dashboard' />
      </div>
      <div className='file-upload-container'>
        <div style={{ paddingTop: '1em' }}>Company *</div>
        <div>
          <select
            id='company'
            name='company'
            defaultValue={
              location.state.Company ? location.state.Company : props.company
            }
            disabled={location.state.Company ? true : false}
            required
          >
            <option value='HPCL' selected={props.company === 'HPCL'}>
              HPCL
            </option>
            <option value='IOCL' selected={props.company === 'IOCL'}>
              IOCL
            </option>
            <option value='GRK' selected={props.company === 'GRK'}>
              GRK
            </option>
            <option value='BPL' selected={props.company === 'BPL'}>
              BPL
            </option>
          </select>
        </div>
        <div style={{ paddingTop: '1em' }}>Version</div>
        <div>
          <input
            type='text'
            id='version'
            defaultValue={
              location.state.Version ? location.state.Version : props.version
            }
            disabled={location.state.Version ? true : false}
            required
          />
        </div>
        <div style={{ paddingTop: '1em' }}>Training *</div>
        <div>
          <select
            style={{ width: '20%' }}
            id='training'
            name='training'
            defaultValue={
              location.state.Training ? location.state.Training : props.training
            }
            disabled={location.state.Training ? true : false}
            required
          >
            <option
              value='file management'
              selected={props.training === 'file management'}
            >
              file management
            </option>
            <option
              value='process management'
              selected={props.training === 'process management'}
            >
              process management
            </option>
            <option
              value='security management'
              selected={props.training === 'security management'}
            >
              security management
            </option>
            <option
              value='device management'
              selected={props.training === 'device management'}
            >
              device management
            </option>
          </select>
        </div>
        <div style={{ paddingTop: '1em' }}>Required Minimum Version *</div>
        <div>
          <input id='reqmin' type='text' required />
        </div>
        <div style={{ paddingTop: '1em' }}>Select Files To Upload *</div>
        <label className='fileContainer'>
          <div>
            <input
              className='uploadfilebox'
              type='file'
              style={{ borderStyle: 'dashed', width: '18em' }}
              id='upload-file'
              accept='application/pdf'
              required
            />
          </div>
        </label>
        <div>
          <button
            id='uploadbutton'
            className='uploadbutton'
            onClick={uploadFiletoServer}
          >
            Upload
          </button>
        </div>
      </div>
    </div>
  );
}

export default UploadFile;
