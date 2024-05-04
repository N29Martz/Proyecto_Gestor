
import { constants } from '../helpers/constants';

export const getLogs = async () => {

  const { API_URL } = constants();
  const user = JSON.parse(localStorage.getItem('user')) || null;

  if (!user)
    return [];

  try {
    
    const response = await fetch(`${ API_URL }/logs`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${ user.token }`,
      },
    });

    if (!response.ok)
      throw new Error('Error fetching logs');

    const data = await response.json();
    return data;

  } catch (error) {
   
    console.error('Error fetching logs:', error);
    return []; // Return an empty array on error for clarity
  
  }
};
