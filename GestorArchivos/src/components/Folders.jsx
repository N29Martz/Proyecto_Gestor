
import React from 'react';
import { getFolders } from '../hooks/getFolders';
import { FolderItem } from './FolderItem';
import PropTypes from 'prop-types';

export const Folders = ({ folders }) => {
  
  // const folders = getFolders();

  console.log('folders', folders)

  return (
    <>
      <div key="items" className="grid grid-cols-5 gap-5">

        { 
          folders.map((folder) => (
            <FolderItem key={ folder.id } id={ folder.id } name={ folder.nombre } />
          ))
        }

      </div>
    </>
  )
};

Folders.prototype = {
  folders: PropTypes.array.isRequired
};
