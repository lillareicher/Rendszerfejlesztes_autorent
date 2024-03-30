import { useEffect, useState } from 'react';

//useEffect(() => {
//    const token = localStorage.getItem('token');
//    fetch('https://localhost:7045/api/cars', {
//        headers: {
//            'Authorization': `Bearer ${token}`
//        }
//    })
//        .then(response => response.json())
//        .then(data => {
//            // handle response data
//        })
//        .catch(error => {
//            console.error('Error fetching cars:', error);
//        });
//}, []);


function Cars() {

    return (
        <div>
            <table border="1">
                <tbody>
                    <tr>
                        <td >
                        </td>
                        <td >Brand
                        </td>
                        <td >Model
                        </td>
                        <td >Daily Price
                        </td>
                    </tr>
                    <tr>
                        <td >1.
                        </td>
                        <td >Toyota
                        </td>
                        <td >Camry
                        </td>
                        <td >$50
                        </td>
                    </tr>
                    <tr>
                        <td >2.
                        </td>
                        <td >Honda
                        </td>
                        <td >Civic
                        </td>
                        <td >$45
                        </td>
                    </tr>
                    <tr>
                        <td >3.
                        </td>
                        <td >BMW
                        </td>
                        <td >3 Series
                        </td>
                        <td >$65
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );

}

export default Cars;
