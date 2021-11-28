import axios from 'axios';
export default function FilterData(company,version,training) {
  console.log('filter the data');
  let ApiUrl =
    'http://localhost:5000/api/DataBaseLayer/getFilteredData/?filterParameters=101|1.8|1001';
  let DefaultValue = axios.get(ApiUrl);
    console.log(DefaultValue);
  return DefaultValue;
}
