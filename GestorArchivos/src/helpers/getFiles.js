
import { constants } from '../helpers/constants';

export const getFiles = async (id) => {

  const { API_URL } = constants();
  const user = JSON.parse(localStorage.getItem('user')) || null;

  if (!user)
    return [];

  try {
    
    const response = await fetch(`${ API_URL }/folders/files/${ id }`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${ user.token }`,
      },
    });

    if (!response.ok)
      throw new Error('Error fetching folders');

    const data = await response.json();
    return data;

  } catch (error) {
   
    console.error('Error fetching files:', error);
    return []; // Return an empty array on error for clarity
  
  }
};
