
import { useContext, useEffect, useState } from 'react';
import { Folders } from '../components/Folders';
import Modal from '../components/Modal';
import { Navbar } from '../components/Navbar';
import { getFiles } from '../helpers/getFiles';
import { AuthContext } from '../context/index';
import { useParams } from 'react-router-dom';
import { FileItem } from '../components/FileItem';

export const FolderPage = () => {

	// obtener el id de la prop
	const { id } = useParams();
	

  const [ files, setFiles ] = useState([]);
  const { refreshToken } = useContext(AuthContext);
	const [ noFiles, setNoFiles ] = useState(false);

  const allFiles = async () => {

    const { data } = await getFiles(id);

		if (!data) return setNoFiles(true);

    setFiles(data);
		setNoFiles(false);

  };

  useEffect(() => {

    refreshToken();
    allFiles();

  }, []);

  const onAddFile = (file) => {

    setFiles([...files, file]);
    setNoFiles(false);

  };

  return (
    <>
      <div>
        <Navbar onNewFile={ onAddFile } />
      </div>

			{/* mostrar un mensaje en caso de no haber archivos */}
			{ noFiles && 
				<h1 className="p-5 text-center text-3xl">
					Esta carpeta no tiene archivos...
				</h1> 
			}

      {/* <Folders folders={ folders } /> */}
			<div key="items" className="grid grid-cols-4 gap-4">

        { 
          files &&
          files.map((file) => (
            <FileItem key={ file.id } { ...file } />
          ))
        }

      </div>
     
    </>
  );
}
