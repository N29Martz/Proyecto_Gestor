import { useState } from "react";

const Modal = ({onCreate, onClose, visible}) => {

  if (!visible) {
    return null;
  }

  const [name, setName] = useState('');
  const [isOpen, setIsOpen] = useState(false);

  const handleOpen = () => {
    setIsOpen(true);
  };

  const handleClose = () => {
    onClose();
    //setIsOpen(false);
  };

  const handleCreate = () => {
    onCreate(name);
    handleClose();
  }

  return (
    <div className="fixed inset-0 bg-black bg-opacity-10 backdrop-blur-sm flex justify-center items-center">
      <div className="bg-[#c53678] p-2 rounded w-72">
          <h2 className="text-white p-1 font-semibold">Nueva Carpeta</h2>
            <div className="flex flex-col space-y-2 border border-white p-1 rounded-md ">
                <input type="text"
                  className="bg-transparent border-none w-full
                  text-white mr-3 py-1 px-2 leading-tight focus:outline-none placeholder-white"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  placeholder="Nombre de la Carpeta"
                />
            </div>
              <button className="flex-shrink-0 text-white py-1 px-2 mr-2" onClick={handleCreate}>Crear</button>
              <button className="flex-shrink-0 text-white py-1 px-2" onClick={handleClose}>Cancelar</button>
      </div>
    </div>
  )
}

export default Modal;