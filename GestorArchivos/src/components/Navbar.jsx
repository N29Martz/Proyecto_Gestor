import { useState } from "react";
import Modal from "./Modal";
import JSZip from 'jszip'
import { constants } from "../helpers/constants";
import PropTypes from 'prop-types';
import ModalFileSelect from "./ModalFileSelect";
import { useParams } from "react-router-dom";

export const Navbar = ({ onNewFolder, onNewFile }) => {

    const [dropdownOpen, setDropdownOpen] = useState(false);
    const [submenuOpen, setSubmenuOpen] = useState(false);
    const [showModal, setShowModal] = useState(false);
    const [showModalFile, setShowModalFile] = useState(false);
    const [file, setFile] = useState(null);
    const { id } = useParams();

    // const [folders, setFolders] = useState([]);

    const [user, setUser] = useState(JSON.parse(localStorage.getItem('user')) || null);

    const { API_URL } = constants();

    const handleCloseModal = () => {
        setShowModal(false);
    }

    const handleCreateModal = async (name) => {

        // console.log(`Crear carpeta con nombre: ${name}`)

        try {

            const data = {
                nombre: name,
                carpetaPadreId: id ? id : null
            };

            const response = await fetch(`${API_URL}/folders`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${user.token}`
                },
                body: JSON.stringify(data)
            }).then(res => res.json()).catch(err => console.error(err));

            // if (!response.ok)
            //    throw new Error(response);

            // const res = await response.json();

            if (!response.status)
                throw new Error(response.message);

            onNewFolder(response.data);

        } catch (error) {

            alert(error);

        }

    }

    const handleFileChange = (event) => {
        setFile(event.target.files[0]);
        setShowModalFile(true);
    }

    const handleFileUp = () => {

        if (!file)
            return console.log("No se ha seleccionado ningún archivo.");

        console.log("Archivo seleccionado:", file.name);

        setShowModalFile(true);

    };

    const handleFolderSelect = (event) => {
        if (file) {
            // Acción a realizar cuando se ha seleccionado una carpeta
            console.log("Carpeta seleccionado:", file.name);
        } else {
            console.log("No se ha selcionado ninguna carpeta.");
        }
    };

    return (
        <nav className="bg-[#C53678] text-white flex h-14 justify-between items-center">
            {/*<div className="flex items-center justify-between">

                    {/* boton menu */}
            <div className="flex-1 px-2 lg:flex-none relative">
                <button type="button"
                    className="text-[#ff5841] hover:text-[#F04D35]"
                    onClick={() => setDropdownOpen(!dropdownOpen)}
                    aria-haspopup="tree"
                    aria-expanded={dropdownOpen ? 'true' : 'false'}>
                    <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-10 h-10">
                        <path strokeLinecap="round" strokeLinejoin="round" d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
                    </svg>
                </button>
                {dropdownOpen && (
                    <div className="absolute mt-2 w-48 rounded-md shadow-lg bg-[#C53678] ring-1 ring-black ring-opacity-5">
                        <div className="relative py-1 min-w-80">
                            <button className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2"
                                id="sub-menu"
                                aria-haspopup="true"
                                aria-expanded={submenuOpen ? 'true' : 'false'}
                                role="menuitem"
                                onClick={() => setSubmenuOpen(!submenuOpen)}>
                                <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-5 h-5">
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" />
                                </svg>
                                <span>Nuevo</span>
                            </button>
                            {submenuOpen && (
                                <div className="absolute mt-2 w-48 rounded-md shadow-lg bg-[#C53678] ring-1 ring-black ring-opacity-5 top-full left-0" role="menu" aria-orientation="vertical" aria-labelledby="submenu">
                                    <div className="py-1" role="menuitem">
                                        <div>
                                            <button className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2"
                                                onClick={() => setShowModal(true)}>
                                                <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                                    <path strokeLinecap="round" strokeLinejoin="round" d="M12 10.5v6m3-3H9m4.06-7.19-2.12-2.12a1.5 1.5 0 0 0-1.061-.44H4.5A2.25 2.25 0 0 0 2.25 6v12a2.25 2.25 0 0 0 2.25 2.25h15A2.25 2.25 0 0 0 21.75 18V9a2.25 2.25 0 0 0-2.25-2.25h-5.379a1.5 1.5 0 0 1-1.06-.44Z" />
                                                </svg>
                                                <span>Nueva Carpeta</span>
                                            </button>
                                            {showModal && (
                                                <Modal onCreate={handleCreateModal} onClose={handleCloseModal} visible={showModal} />
                                            )}
                                            <button className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" onClick={() => document.getElementById('fileInput').click()}>
                                                <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                                    <path strokeLinecap="round" strokeLinejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 0 0-3.375-3.375h-1.5A1.125 1.125 0 0 1 13.5 7.125v-1.5a3.375 3.375 0 0 0-3.375-3.375H8.25m6.75 12-3-3m0 0-3 3m3-3v6m-1.5-15H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 0 0-9-9Z" />
                                                </svg>
                                                <span>Subir archivo</span>
                                                <input type="file" id="fileInput" onChange={handleFileChange} className="hidden" />
                                            </button>
                                            <button className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" onClick={() => document.getElementById('folderInput').click()}>
                                                <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                                    <path strokeLinecap="round" strokeLinejoin="round" d="M3 16.5v2.25A2.25 2.25 0 0 0 5.25 21h13.5A2.25 2.25 0 0 0 21 18.75V16.5m-13.5-9L12 3m0 0 4.5 4.5M12 3v13.5" />
                                                </svg>
                                                <span>Subir carpeta</span>
                                                <input type="file" id="folderInput" onChange={handleFileChange} className="hidden" />
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </div>
                        <div className="py-1" role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                            <a href="/" className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" role="menuitem">
                                <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-5 h-5">
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 0 0-3.375-3.375h-1.5A1.125 1.125 0 0 1 13.5 7.125v-1.5a3.375 3.375 0 0 0-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 0 0-9-9Z" />
                                </svg>
                                <span>Archivos</span>
                            </a>
                            <a href="recycle" className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" role="menuitem">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth={2} strokeLinecap="round" strokeLinejoin="round" className="w-5 h-5">
                                    <polyline points="3 6 5 6 21 6"></polyline>
                                    <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                                    <line x1="10" y1="11" x2="10" y2="17"></line>
                                    <line x1="14" y1="11" x2="14" y2="17"></line>
                                </svg>
                                <span>Papelera</span>
                            </a>
                            <a href="/record" className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" role="menuitem">
                                <svg viewBox="0 0 24 24" fill="none" stroke="#ffffff" className="w-5 h-5">
                                    <g id="SVGRepo_bgCarrier" strokeWidth="0" />
                                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round" />
                                    <g id="SVGRepo_iconCarrier"> 
                                        <path d="M2 12C2 17.5228 6.47715 22 12 22C13.8214 22 15.5291 21.513 17 20.6622M12 2C17.5228 2 22 6.47715 22 12C22 13.8214 21.513 15.5291 20.6622 17" 
                                            stroke="#ffffff" strokeWidth="1.5" strokeLinecap="round" /> 
                                        <path d="M12 9V13H16" stroke="#ffffff" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" /> 
                                        <path d="M17 20.6622C15.5291 21.513 13.8214 22 12 22C6.47715 22 2 17.5228 2 12C2 6.47715 6.47715 2 12 2C17.5228 2 22 6.47715 22 12C22 13.8214 21.513 15.5291 20.6622 17" 
                                            stroke="#ffffff" strokeWidth="1.5" strokeLinecap="round" strokeDasharray="0.5 3.5" /> 
                                    </g>
                                </svg>
                                <span>Historial</span>
                            </a>
                            <a href="/profile" className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" role="menuitem">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth={2} strokeLinecap="round" strokeLinejoin="round" className="w-5 h-5">
                                    <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path>
                                    <circle cx="12" cy="7" r="4"></circle>
                                </svg>
                                <span>Perfil</span>
                            </a>
                            <a href="/login" className="px-4 py-2 text-sm text-white hover:bg-[#d93682] hover:text-white flex items-center space-x-2" role="menuitem">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth={2} strokeLinecap="round" strokeLinejoin="round" className="w-5 h-5">
                                    <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"></path>
                                    <polyline points="16 17 21 12 16 7"></polyline>
                                    <line x1="21" y1="12" x2="9" y2="12"></line>
                                </svg>
                                <span>Cerrar Sesión</span>
                            </a>
                        </div>
                    </div>
                )}
            </div>

            {/* etiqueta filesync */}
            <div className="flex justify-between items-center">
                <a className="text-lg font-bold flex items-center space-x-3 rtl:space-x-reverse">
                    <span className="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">FileSync</span>
                </a>
            </div>

            {/* Boton buscar */}
            <div className="flex md:order-2 space-x-3 md:space-x-0 rtl:space-x-reverse p-2 items-center">
                <button type="button" className="text-[#ff5841] hover:text-[#F04D35]">
                    <svg fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-10 h-10">
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
                    </svg>
                </button>
            </div>

            {/*</div>*/}
            {/* llamar el modal de ModalFileSelect.jsx */}
            {showModalFile && (
                <ModalFileSelect selectedFile={file} openModal={showModalFile} onNewFile={onNewFile} />
            )}

        </nav>
    )
}

Navbar.propTypes = {
    onNewFolder: PropTypes.func,
    onNewFile: PropTypes.func
};
