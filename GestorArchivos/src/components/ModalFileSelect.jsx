
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { constants } from '../helpers/constants';
import PropTypes from 'prop-types';

export default function ModalFileSelect({ selectedFile, openModal, onNewFile }) {

	const [ isOpen, setIsOpen ] = useState(false);
	const [ fileContent, setFileContent ] = useState(null);
	const [ fileName, setFileName ] = useState('');
	const { id } = useParams();

	const { API_URL } = constants();

	const user = JSON.parse(localStorage.getItem('user')) || null;

  if (!user)
    return [];

	const uploadFile = async () => {

		try {

			// la info se envia por Form y con el campo de "nombre" y la carpetetaId
			const formData = new FormData();

			formData.append('Archivo', selectedFile);
			formData.append('Nombre', fileName);

			// de las props se obtiene el id de la carpeta
			formData.append('CarpetaId', id);
			
			const response = await fetch(`${API_URL}/archivos`, {
				method: 'POST',
				headers: {
					// 'Content-Type': 'multipart/form-data',
					'Authorization': `Bearer ${ user.token }` 
				},
				body: formData
			});

			if (!response.ok) 
				throw new Error('Error al subir el archivo');
		
			const res = await response.json();

			alert(res.message);

			onNewFile(res.data);

			closeModal();
			
		} catch (error) {
			
			console.error(error);

		}

	};

	const closeModal = () => {
    setIsOpen(false);
  }

	useEffect(() => {

    if (selectedFile) {
      
      const reader = new FileReader();

      reader.onload = (event) => {
        setFileContent(event.target.result);
      };

      reader.readAsDataURL(selectedFile);

			setFileName(selectedFile.name);

    }

  }, [selectedFile]);

  useEffect(() => {
    if (openModal) {
      setIsOpen(true);
    }
  }, [openModal]);

	return (
		<>
			{isOpen && (
				<div className="fixed inset-0 bg-black bg-opacity-10 backdrop-blur-sm flex justify-center items-center">
					<div className="max-w-sm rounded overflow-hidden shadow-lg">
						<img className="w-64 h-auto" src={ fileContent } alt={ selectedFile.name } />
						<div className="px-6 py-4">

							{/* input text con el nombre de; archivo */}
							<div className="mb-4">
								<label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="username">
									Nombre del archivo
								</label>
								<input
									className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
									id="username"
									type="text"
									placeholder="Nombre del archivo"
									value={ fileName }
									onChange={ (e) => setFileName(e.target.value) }
								/>
							</div>

							{/* 2 botones uno apara guardar y otro para cerrar el modal */}

							<div className="">
								<button
									className="m-2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
									type="button"
									onClick={ uploadFile }
								>
									Guardar
								</button>
								<button
									className="m-2 bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
									onClick={ closeModal }
									type="button"
								>
									Cerrar
								</button>
							</div>

						</div>
							
					</div>
				</div>
			)}
		</>
	);

};

ModalFileSelect.propTypes = {
	selectedFile: PropTypes.object.isRequired,
	openModal: PropTypes.bool.isRequired,
	onNewFile: PropTypes.func.isRequired
};
