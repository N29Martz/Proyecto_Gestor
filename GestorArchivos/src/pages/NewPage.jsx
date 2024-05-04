import { useEffect, useState } from 'react'
import { Navbar } from '../components/Navbar'

export const NewPage = () => {

  const [data, setData] = useState([]);

  const getData = async () => {
    const response = await fetch('fakedata/MOCK_DATA.json');
    const jsonData = await response.json();
    setData(jsonData.data);
  }

  useEffect(() => {
    getData();
  }, []);

  return (
    <div>
      <Navbar />
      <div className='flex justify-center'>
        <h1 className='text-[#ff5841] font-bold'>Prueba Historial</h1>
      </div>
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Description</th>
              <th>User ID</th>
              <th>Date Time</th>
              <th>File ID</th>
              <th>Log Type ID</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item) => (
              <tr key={item.id}>
                <td>{item.id}</td>
                <td>{item.description}</td>
                <td>{item.userId}</td>
                <td>{item.dateTime}</td>
                <td>{item.fileId}</td>
                <td>{item.logTypeId}</td>
              </tr>
            ))}
          </tbody>
        </table>
    </div>
  )
}
