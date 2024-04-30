
import { useNavigate } from "react-router-dom"
import { constants } from "../helpers/constants";
import { AuthContext } from "../context/index";
import { useContext, useEffect, useState } from "react";
import { InputEmailValidation } from "../validations/input-email";
import { InputRequiredValidation } from "../validations/input-required";

export const LoginPage = () => {

    const [loginForm, setLoginForm] = useState({
        email: '',
        password: ''
    })

    const navigate = useNavigate();

    const [errors, setErrors] = useState([]);
    const { login, refreshToken } = useContext(AuthContext);

    useEffect(() => {
        refreshToken();
    }, []);

    const { API_URL } = constants();

    const handleSubmit = async (e) => {
        e.preventDefault();

        let newErrors = [];

        const errorEmail = InputEmailValidation('Correo ELectrónico', loginForm.email);
        if(!errorEmail.validation) {
            newErrors.push(errorEmail.message);
        }

        const errorEmailRequired = InputRequiredValidation('Correo ELectrónico', loginForm.email);
        if(!errorEmailRequired.validation) {
            newErrors.push(errorEmailRequired.message);
        }

        const errorPasswordRequired = InputRequiredValidation('Contraseña', loginForm.password);
        if(!errorPasswordRequired.validation) {
            newErrors.push(errorPasswordRequired.message);
        }

        setErrors(newErrors);

        //console.log(errors);
        if(errors.length === 0) {
            //console.log('no hay errores')
            try {
                const response = await fetch(`${API_URL}/auth/login`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(loginForm)
                });

                if(!response.ok) {
                    throw new Error('Error en el inicio de sesión');
                } 

                const result = await response.json();
                login(result.data);

            } catch (error) {
                console.log(error);  
            }

        }


    }

    const handleSignUp = () => {
        navigate('/signup')
    }

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
                            onClick={handleSignUp}
                            className="py-2 bg-[#ff5841] text-white px-8 hover:bg-[#F04D35] font-bold uppercase rounded-md">Registrar</button>
                    </div>

                </nav>
            </div>

            <div className="h-full antialiased container flex justify-center items-center pb-5 p-10">
                <div className="flex bg-[#F2EFEF] items-center rounded-lg p-7">
                    <img src="/img/acceso.png" alt="" className="w-72 h-full float-left mr-10" />
                    <div className="w-1/2 container flex justify-center items-center py-2">
                        <form id="formlogin" onSubmit={ handleSubmit }>
                            <div className="flex flex-col p-10 space-y-6 border-[#F2EFEF]">
                                <label htmlFor="username" className="block text-[#ff5841] font-bold uppercase">Correo Electrónico</label>
                                <div className="flex flex-col justify-center rounded w-80 space-y-1 border border-[#ff5841]">
                                    <input type="email" className="appearance-none bg-transparent bg-[#F2EFEF] border-none 
                                        w-full text-black mr-3 py-2.5 px-2 leading-tight focus:outline-none"
                                        id="username" value={ loginForm.email } onChange={ (e) => setLoginForm({ ...loginForm, email: e.target.value }) } />
                                </div>

                                <label htmlFor="password" className="block text-[#ff5841] font-bold uppercase">Contraseña</label>
                                <div className="flex flex-col justify-center rounded space-y-1 border border-[#ff5841]">
                                    <input type="password"
                                        className="appearance-none bg-transparent bg-[#F2EFEF] border-none 
                                        w-full text-gray-300 mr-3 py-2.5 px-2 leading-tight focus:outline-none"
                                        id="password" value={ loginForm.password } onChange={ (e) => setLoginForm({ ...loginForm, password: e.target.value }) } />
                                </div>

                                <button type="submit"
                                    className="py-2 bg-[#ff5841] text-white px-8 hover:bg-[#F04D35] font-bold uppercase rounded-md">Iniciar Sesión    
                                </button>
                            </div>
                        </form>

                    </div>

                </div>

            </div>

        </div>
    )
}

export default LoginPage
