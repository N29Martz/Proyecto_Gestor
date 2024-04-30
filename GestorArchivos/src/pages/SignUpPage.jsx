
import { useNavigate } from "react-router-dom";
import { InputEmailValidation } from '../validations/input-email';
import { useState } from "react";
import { constants } from "../helpers/constants";

export const SignUpPage = () => {

  const navigate = useNavigate();

  const [ form, setForm ] = useState({
    nombre: '',
    apellido: '',
    email: '',
    password: '',
    confirmPassword: ''
  })

  const handleLogin = () => {
    navigate('/login')
  }

  const handleSignUp = async (e) => {

    e.preventDefault();

    if (!form.nombre || !form.apellido || !form.email || !form.password || !form.confirmPassword)
      return alert('Todos los campos son obligatorios');

    // crea validaciones para los campos del formulario, del email de /validations/input-email.js
    const emailValidation = InputEmailValidation('Correo Electrónico', form.email);
    
    if (!emailValidation)
      return alert(emailValidation.message);

    // valida que los campos no estén vacíos

    // valida que las contraseñas sean iguales
    if (form.password !== form.confirmPassword)
      return alert('Las contraseñas no coinciden');
      
    // mandar la información al backend
    try {

      const { API_URL } = constants();

      const response = await fetch(`${ API_URL }/usuarios`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(form)
      });

      const data = await response.json();

      if (response.ok)
        alert(data.message),
        navigate('/login');
       
      alert(data.message);

    } catch (error) {
      
      console.error(error);

    }

  };

  return (
    <div>
      <div>
        <nav className="bg-[#C53678] text-white flex justify-between items-center">
          <div className="flex-1 px-2 lg:flex-none">
            <a className="text-lg font-bold flex items-center space-x-3 rtl:space-x-reverse">
              <span className="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">FileSync</span>
            </a>
          </div>

          <div className="flex md:order-2 space-x-3 md:space-x-0 rtl:space-x-reverse p-2">
            <button type="button"
              onClick={handleLogin}
              className="py-2 bg-[#ff5841] text-white px-8 hover:bg-[#F04D35] font-bold uppercase rounded-md">Iniciar Sesión</button>
          </div>

        </nav>
      </div>

      <div className="h-full antialiased container flex justify-center items-center pb-5 p-10">
        <div className="flex bg-[#F2EFEF] items-center rounded-lg p-7">
          <img src="/img/sitio.png" alt="" className="w-72 h-full float-left mr-10" />
          <div className="w-1/2 container flex justify-center items-center py-2">
            <form id="formSignUp">
              <div className="flex flex-col p-10 space-y-6 border-[#F2EFEF]">
                <div className="flex flex-col space-y-1 w-72 border-b-2 border-[#ff5841]">
                  <input type="text"
                    className="appearance-none bg-transparent border-none w-full
                    text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                    placeholder="Nombre"
                    value={ form.nombre }
                    onChange={ (e) => setForm({ ...form, nombre: e.target.value }) }
                  />
                </div>
                <div className="flex flex-col space-y-1 w-72 border-b-2 border-[#ff5841]">
                  <input type="text"
                    className="appearance-none bg-transparent border-none w-full
                    text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                    placeholder="Apellido"
                    value={ form.apellido }
                    onChange={ (e) => setForm({ ...form, apellido: e.target.value }) }
                  />
                </div>
                <div className="flex flex-col space-y-1 w-72 border-b-2 border-[#ff5841]">
                  <input type="email"
                    className="appearance-none bg-transparent border-none w-full
                    text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                    placeholder="Correo Electrónico"
                    value={ form.email }
                    onChange={ (e) => setForm({ ...form, email: e.target.value }) }
                  />
                </div>
                <div className="flex flex-col space-y-1 w-72 border-b-2 border-[#ff5841]">
                  <input type="password"
                    className="appearance-none bg-transparent border-none w-full
                    text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                    placeholder="Contraseña"
                    value={ form.password }
                    onChange={ (e) => setForm({ ...form, password: e.target.value }) }
                  />
                </div>
                <div className="flex flex-col space-y-1 w-72 border-b-2 border-[#ff5841]">
                  <input type="password"
                    className="appearance-none bg-transparent border-none w-full
                    text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                    placeholder="Confirmación de Contraseña"
                    value={ form.confirmPassword }
                    onChange={ (e) => setForm({ ...form, confirmPassword: e.target.value }) }
                  />
                </div>
                <button type="submit"
                    className="py-2 bg-[#ff5841] text-white px-8 hover:bg-[#F04D35] font-bold uppercase rounded-md"
                    onClick={ handleSignUp }
                  >
                  Registrar
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>

    </div>
  )
}
