import axios from 'axios';
export default function FileCheck() {
  console.log('starting up everything');
  let ApiUrl =
    'http://localhost:5000/api/DataBaseLayer/getFileCheck/';
  let DefaultValue = axios.get(ApiUrl);
    console.log(DefaultValue);
  return DefaultValue;
}
