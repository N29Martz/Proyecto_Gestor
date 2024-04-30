
export const InputRequiredValidation = (name, value) => {

    if(value.length == 0 || value === '')
        return {
            validation: false,
            message: `${name} es requerido.`
        };

    return {
        validation: true
    };

}
