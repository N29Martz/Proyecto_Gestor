
export const InputEmailValidation = (name, value) => {

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if(!emailRegex.test(value)) {
        return {
            validation : false,
            message: `${name} debe ser un correo electrónico válido.` 
        }
    } else {
        return {
            validation : true
        }
    }

} 
