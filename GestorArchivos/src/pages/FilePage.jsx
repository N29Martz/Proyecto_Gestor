import { useContext, useEffect, useState } from 'react';
import { Folders } from '../components/Folders';
import Modal from '../components/Modal';
import { Navbar } from '../components/Navbar';
import { getFolders } from '../helpers/getFolders';
import { AuthContext } from '../context/index';
import { FolderItem } from '../components/FolderItem';
import { folder } from 'jszip';

export const FilePage = () => {

  const [ folders, setFolders ] = useState([]);
  const { refreshToken } = useContext(AuthContext);
  const [ error, setError ] = useState(false);

  const allFolders = async () => {

    const { data } = await getFolders();

    if (!data)
      return setError(true);

    setFolders(data);
    setNoFiles(false);

  };

  useEffect(() => {

    refreshToken();
    allFolders();

  }, []);

  const onAddFolder = (folder) => {

    setFolders([...folders, folder]);
    setError(false);

  };

  return (
    <>
      <div>
        <Navbar onNewFolder={ onAddFolder } />
      </div>

      { error && 
        <h1 className="p-5 text-center text-3xl">
					No hay carpetas...
				</h1>  
      }

      {/* <Folders folders={ folders } /> */}

      <div key="items" className="grid grid-cols-5 gap-5">

        { 
          folders &&
          folders.map((folder) => (
            <FolderItem key={ folder.id } { ...folder } />
          ))
        }

      </div>     
    </>
  );
}

