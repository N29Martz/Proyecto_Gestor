
import { useEffect, useState } from 'react';
import { constants } from '../helpers/constants';

export const getFolders = () => {
  
	try {

		const { API_URL } = constants();

		const [ folders, setFolders ] = useState([]);
		const [ user, setUser ] = useState(JSON.parse(localStorage.getItem('user')) || null);


		const getFolders = async () => {

			console.log(user.token);

			const response = await fetch(`${API_URL}/folders/user`, {
				method: 'GET',
				headers: {
					'Content-Type': 'application/json',
					'Authorization': `Bearer ${ user.token }`
				}
			});
			
			if (!response.ok)
				throw new Error('Error fetching folders');
			
			const { data } = await response.json();

			setFolders(data);

		};

		useEffect(() => {

			getFolders();

		}, []);

		return folders;

	} catch (error) {

		console.error(error);

		return [];
			
	}

};
