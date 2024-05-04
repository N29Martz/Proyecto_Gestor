
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { constants } from '../helpers/constants';

export const FolderItem = ({ id, nombre }) => {

	const [showMenu, setShowMenu] = useState(false);
	const [fetched, setFechet] = useState(true);
	const [newName, setNewName] = useState(nombre);
	const [editMode, setEditMode] = useState(false);

	const { API_URL } = constants();

	const user = JSON.parse(localStorage.getItem('user')) || null;

	const handleEdit = () => {
		setEditMode(true);
	};

	const handleSaveEdit = async () => {
		try {
			const formData = new FormData();
			formData.append('nombre', newName);

			const response = await fetch(`${API_URL}/archivos/${id}`, {
				method: 'PUT',
				headers: {
					'Authorization': `Bearer ${user.token}`
				},
				body: formData
			});

			if (!response.ok) {
				throw new Error('Error al actualizar el nombre del archivo');
			}

			const res = await response.json();

			alert(res.message);

			setEditMode(false);

		} catch (error) {
			console.error(error);
		}
	}

	const handleCancelEdit = () => {
		setNewName(nombre);
		setEditMode(false);
	}

	const handleNameChange = (event) => {
		setNewName(event.target.value);
	};

	const handleDelete = async () => {
		try {
			const response = await fetch(`${API_URL}/folders/${id}`, {
				method: 'DELETE',
				headers: {
					'Authorization': `Bearer ${user.token}`
				}
			});

			if (!response.ok) {
				throw new Error('Error al borrar carpeta');
			}

			const res = await response.json();

			alert(res.message);

		} catch (error) {
			console.error(error);
		}
	}

	return (
		<div key={id} id={id} className="flex justify-center content-center mt-5">
			<div className='flex justify-center content-center mt-5'
				onMouseOver={() => setShowMenu(true)}
				onMouseOut={() => setShowMenu(false)}
			>
				<div className="max-w-sm p-6 bg-white border border-gray-200 rounded-lg shadow">
					<svg
						xmlns="http://www.w3.org/2000/svg"
						width="48"
						height="48"
						viewBox="0 0 24 24"
					>
						<path
							fill="currentColor"
							d="M4 20q-.825 0-1.412-.587T2 18V6q0-.825.588-1.412T4 4h6l2 2h8q.825 0 1.413.588T22 8v10q0 .825-.587 1.413T20 20zm0-2h16V8h-8.825l-2-2H4zm0 0V6z"
						/>
					</svg>
					<a href={`/folder/${id}`}>
						{editMode ? (
							<input
								type="text"
								value={newName}
								onChange={handleNameChange}
								className="mb-2 text-2xl font-semibold tracking-tight text-gray-900 w-full max-w-xs mx-auto"
							/>
						) : (
							<h5 className="mb-2 text-2xl font-semibold tracking-tight text-gray-900">{nombre}</h5>
						)}
					</a>
					{showMenu && (
						<div className='flex justify-center mt-2'>
							{editMode ? (
								<>
									<button
								onClick={handleSaveEdit}
								className="bg-[#C53678] text-white active:bg-[#d44d99] font-bold uppercase text-xs px-4 py-2 rounded shadow hover:shadow-md outline-none focus:outline-none mr-1 mb-1"
								type="button"
								style={{ transition: 'all .15s ease' }}
							>
								<svg viewBox="0 0 20 20" fill="#000000" className="w-5 h-5">
									<g id="SVGRepo_bgCarrier" strokeWidth="0" />
									<g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round" />
									<g id="SVGRepo_iconCarrier">
										 <path fill="#ffffff" d="M2.72727273,1.36363636 C1.97415716,1.36363636 1.36363636,1.97415716 1.36363636,2.72727273 L1.36363636,17.2727273 C1.36363636,18.0258428 1.97415716,18.6363636 2.72727273,18.6363636 L17.2727273,18.6363636 C18.0258428,18.6363636 18.6363636,18.0258428 18.6363636,17.2727273 L18.6363636,2.72727273 C18.6363636,1.97415716 18.0258428,1.36363636 17.2727273,1.36363636 L2.72727273,1.36363636 Z M17.2727273,0 C18.7789584,0 20,1.22104159 20,2.72727273 L20,17.2727273 C20,18.7789584 18.7789584,20 17.2727273,20 L2.72727273,20 C1.22104159,20 0,18.7789584 0,17.2727273 L0,2.72727273 C0,1.22104159 1.22104159,0 2.72727273,0 L17.2727273,0 Z M13.9551121,2.43531901 C13.5785543,2.43531901 13.2732939,2.74057941 13.2732939,3.1171372 L13.2732939,3.1171372 L13.2732939,5 C13.2732939,5.37655778 13.5785543,5.68181818 13.9551121,5.68181818 C14.3316699,5.68181818 14.6369303,5.37655778 14.6369303,5 L14.6369303,5 L14.6369303,3.1171372 C14.6369303,2.74057941 14.3316699,2.43531901 13.9551121,2.43531901 Z M2.74352037,0.93895244 L4.10714316,0.945037251 L4.08570734,5.39140486 C4.05609642,5.79699746 4.18569012,6.12543845 4.50077186,6.42819766 C4.81430953,6.72947317 5.24532108,6.86648432 5.88464139,6.82809445 L14.5095222,6.83246994 C14.992486,6.77748966 15.3312375,6.61903949 15.5594272,6.36310688 C15.7883676,6.10633222 15.9058504,5.76031939 15.9048323,5.29028972 L15.9049552,0.941975576 L17.2685916,0.942014114 L17.2685916,5.28882981 C17.2701781,6.07752771 17.0421196,6.74921064 16.57725,7.27059836 C16.1116296,7.79282813 15.4625833,8.09641797 14.5866418,8.19173081 L5.9269026,8.19041981 C4.96826314,8.24995374 4.16175588,7.99357897 3.55595447,7.41146986 C2.95169713,6.83084443 2.66618136,6.10723611 2.72388756,5.33871738 L2.74352037,0.93895244 Z" /> </g>
								</svg>
							</button>
							<button
								onClick={handleCancelEdit}
								className="bg-[#ff5841] text-white active:bg-[#d05151] font-bold uppercase text-xs px-4 py-2 rounded shadow hover:shadow-md outline-none focus:outline-none mr-1 mb-1"
								type="button"
								style={{ transition: 'all .15s ease' }}
							>
								X
							</button>
								</>
							): (
								<>
								<button className='text-[#C53678] hover:text-[#9a3e69]'
									onClick={handleEdit}
								>
									<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6">
										<path strokeLinecap="round" strokeLinejoin="round" d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10" />
									</svg>
								</button>
								<button className='text-[#ff5841] hover:text-[#F04D35]'
									onClick={handleDelete}
								>
									<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6">
										<path strokeLinecap="round" strokeLinejoin="round" d="m14.74 9-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 0 1-2.244 2.077H8.084a2.25 2.25 0 0 1-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 0 0-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 0 1 3.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 0 0-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 0 0-7.5 0" />
									</svg>

								</button>
								
								</>
							)}
						</div>
					)}

				</div>
			</div>

		</div>
	);

};

//reglas para poder recibir las props correctas en el formato correcto
FolderItem.propTypes = {
	id: PropTypes.string.isRequired,
	nombre: PropTypes.string.isRequired,
};
