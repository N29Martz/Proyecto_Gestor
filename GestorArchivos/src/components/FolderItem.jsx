
import React from 'react';
import PropTypes from 'prop-types';

export const FolderItem = ({ id, nombre }) => {

	return (
		<div key={ id } id={  id } className="flex justify-center content-center mt-5">
			<div className="max-w-sm p-6 bg-white border border-gray-200 rounded-lg shadow">
			<svg
				xmlns="http://www.w3.org/2000/svg"
				width="48"
				height="48"
				viewBox="0 0 24 24"
			>
				<path
					fill="currentColor"
					d="M4 20q-.825 0-1.412-.587T2 18V6q0-.825.588-1.412T4 4h6l2 2h8q.825 0 1.413.588T22 8v10q0 .825-.587 1.413T20 20zm0-2h16V8h-8.825l-2-2H4zm0 0V6z"
				/>
			</svg>
				<a href={ `/folder/${ id }` }>
					<h5 className="mb-2 text-2xl font-semibold tracking-tight text-gray-900">{ nombre }</h5>
				</a>
				
			</div>
		</div>
	);

};

FolderItem.propTypes = {
	id: PropTypes.string.isRequired,
	nombre: PropTypes.string.isRequired,
};
