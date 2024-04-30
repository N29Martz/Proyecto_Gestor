

import PropTypes from 'prop-types';

const obtainTypefile = (urlArchivo) => {

	const typeFiles = {
		'Imagen': [ 'jpg', 'jpeg', 'png', 'gif', 'bmp' ],
		'Video': [ 'mp4', 'avi', 'mov', 'mkv', 'flv' ],
		'Audio': [ 'mp3', 'wav', 'ogg', 'flac' ],
		'Documento': [ 'pdf', 'doc', 'docx', 'xls', 'xlsx', 'ppt', 'pptx', 'txt' ],
		'Otro': [ 'zip', 'rar', '7z', 'tar' ]
	};

	const extension = urlArchivo.split('.').pop();

	for (const type in typeFiles) {
		if (typeFiles[type].includes(extension))
			return type;
	}

	return 'Otro';

};

export const FileItem = ({ id, nombre, urlArchivo, tamanio, fechaCreacion }) => {
  
  return (

    <div key={ id } id={ id } className="m-5 max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-100 dark:border-gray-700">
        <div>
            <img className="rounded-t-lg object-cover h-48 w-96" src={ urlArchivo } alt={ nombre } />
        </div>
        <div className="p-5">
            <div>
                <h5 className="truncate mb-2 text-2xl font-bold tracking-tight text-gray-900 text-dark">
									{ nombre }
								</h5>
								{/* colocar el tipo de archivo */}
								{/* <div className=" top-2 right-2 bg-white bg-opacity-50 rounded-lg p-1"> */}
									<p className="mb-3 text-xs font-semibold text-gray-900 dark:text-dark-400">
										{ obtainTypefile(urlArchivo) }
									</p>
								{/* </div> */}
            </div>
            <p className="mb-3 font-normal text-gray-700 dark:text-dark-400">
							{/* { fechaCreacion } */}
							{ new Date(fechaCreacion).toLocaleString() }
						</p>
            
        </div>
    </div>

  );

};

FileItem.propTypes = {
	id: PropTypes.string.isRequired,
	nombre: PropTypes.string.isRequired,
	urlArchivo: PropTypes.string.isRequired,
	tamanio: PropTypes.number.isRequired,
	fechaCreacion: PropTypes.string.isRequired
};
