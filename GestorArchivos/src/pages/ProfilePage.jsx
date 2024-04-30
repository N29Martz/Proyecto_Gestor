import { Navbar } from '../components/Navbar'
import { useRef, useState } from 'react'

export const ProfilePage = () => {

  const inputRef = useRef(null);
  const [imagen, setImagen] = useState("");

  const handleImgClic = () => {
    inputRef.current.click();
  }

  const handleImgChange = (event) => {
    const file = event.target.files[0];
    console.log(file);
    setImagen(event.target.files[0]);
  }

  return (
    <div>
      <Navbar />

      <div>
        <div className='flex flex-row justify-around'>
          <div className='flex flex-col mx-10 mt-10' onClick={handleImgClic}>
            {imagen ? (<img src={URL.createObjectURL(imagen)} alt='' className='w-52 h-52 rounded-full' />) : 
            (<img src="/img/usuario.png" alt="" className='w-52 h-52 ml-9' />)}
            <input type="file" ref={inputRef} onChange={handleImgChange} className='hidden' />

          </div>

          <div className='flex items-center justify-between'>
            <form action="" id='formProfile'>
              <h2 className='text-[#ff5841] font-bold text-2xl mb-2 mt-4'>Perfil</h2>
              <div className='flex flex-row'>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] mx-4 w-72'>
                  <input type="text"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Nombre' />
                </div>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] mx-4 w-72'>
                  <input type="text"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Apellido' />
                </div>
              </div>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] mx-4 w-72'>
                  <input type="date"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Fecha de Nacimiento' />
                </div>
              <h2 className='text-[#ff5841] font-bold text-2xl mb-2 mt-4'>Cambiar Correo electrónico</h2>
              <div className='flex flex-row'>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] mx-4 w-72'>
                  <input type="email"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Correo Electrónico' />
                </div>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] w-72'>
                  <input type="email"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Nuevo Correo Electrónico' />
                </div>
              </div>
              <h2 className='text-[#ff5841] font-bold text-2xl mb-2 mt-4'>Cambiar Contraseña</h2>
              <div className='flex flex-row mb-4'>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] mx-4 w-72'>
                  <input type="password"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Nueva Contraseña' />
                </div>
                <div className='flex flex-col space-y-1 border-b-2 border-[#ff5841] w-72'>
                  <input type="password"
                    className='appearance-none bg-transparent border-none w-full placeholder-[#ff5841]
                        text-[#ff5841] mr-3 py-1 px-2 leading-tight focus:outline-none' placeholder='Confirmar Contraseña' />
                </div>
              </div>
              <button type="submint"
                className="py-2 bg-[#ff5841] text-white px-8 hover:bg-[#F04D35] font-bold uppercase rounded-md mr-2">Guardar Cambios
              </button>
              <button type="submint"
                className="py-2 bg-[#ff5841] text-white px-8 hover:bg-[#F04D35] font-bold uppercase rounded-md">Eliminar Cuenta
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}
