
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

    let { data } = await getFiles(id);

		if (!data) return setNoFiles(true);

    data = data.map((file) => !file.deleted && file ).filter((file) => file);

    if (!data.length)
      return setNoFiles(true);

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

  const onEditFile = (file) => {

    const newFiles = files.map((f) => f.id === file.id ? file : f);
    setFiles(newFiles);

  };

  const onDeleteFile = (file) => {
      
    const newFiles = files.map((f) => f.id === file.id ? { ...f, deleted: true } : f);
    setFiles(newFiles);
  
  }
  
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
            !file.deleted && <FileItem key={ file.id } { ...file } onEditFile={ onEditFile } onDeleteFile={ onDeleteFile } />
          ))
        }

      </div>
     
    </>
  );
}
