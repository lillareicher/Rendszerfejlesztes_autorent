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
    const [carsList, setCarsList] = useState();

    useEffect(() => {
        getCarsList();
    }, []);

    async function getCarsList() {
        try {
            const response = await fetch('api/carscontroller/listcars');
            if (!response.ok) {
                throw new Error('Failed to fetch cars');
            }

            const data = await response.json
            setCarsList(data);
        } catch (error) {
            console.error('Error fetching cars:', error);
        }
    }

    return (
        <div>
            <table border="1">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Brand</th>
                        <th>Model</th>
                        <th>Daily Price</th>
                    </tr>
                </thead>
                <tbody>
                    {carsList.map(car => (
                        <tr key={car.id}>
                            <td>{car.id}</td>
                            <td>{car.brand}</td>
                            <td>{car.model}</td>
                            <td>{car.daily_price}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );

}

export default Cars;
