import React, { useContext, useEffect, useState } from 'react'
import { Navbar } from '../components/Navbar'
import { getLogs } from '../helpers/getLogs';
import { AuthContext } from '../context';

export const RecordPage = () => {

    const [ logs, setLogs ] = useState([]);
    const { refreshToken } = useContext(AuthContext);
	const [ nologs, setNoLogs ] = useState(false);

    const allLogs = async () => {

        let { data } = await getLogs();

        if (!data) return setNoLogs(true);

        setLogs(data);
        setNoLogs(false);

    };
    
    useEffect(() => {
    
        refreshToken();
        allLogs();
    
    }, []);

    return (
        <>
            <div>
                <Navbar />
            </div>

            <h1 className='text-[#ff5841] font-bold p-5 text-center text-3xl'>Historial</h1>

            <div className='overflow-x-auto p-4'>
                <table className='table w-full border-collapse border border-gray-200 bg-[#F2EFEF]'>
                    <thead>
                        <tr>
                            <th className='px-4 py-2 text-left text-[#C53678]'>Usuario</th>
                            <th className='px-4 py-2 text-left text-[#C53678]'>Descripción</th>
                            <th className='px-4 py-2 text-left text-[#C53678]'>Tipo Log</th>
                            <th className='px-4 py-2 text-left text-[#C53678]'>Fecha y Hora</th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* <tr>
                            <td>
                                <div className='flex items-center gap-3'>
                                    <div className='font-bold'>Usuario</div>
                                </div>
                            </td>
                            <td>Neoplasm of uncrt behavior of the major salivary gland, unsp</td>
                            <td>tipo</td>
                            <td>6/3/2023</td>

                        </tr> */}
                        {
                            logs.map((log) => (
                                <tr key={ log.id }>
                                    <td>{ log.userId }</td>
                                    <td>{ log.description }</td>
                                    <td>{ log.logType.description }</td>
                                    <td>{ new Date(log.dateTime).toLocaleString() }</td>
                                </tr>
                            ))
                        }
                    </tbody>
                </table>
            </div>
        </>
    )
}
