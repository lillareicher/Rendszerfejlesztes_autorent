import { useEffect, useState } from 'react';



function Cars() {
    const [carsList, setCarsList] = useState([]);

    const flag = null;

    useEffect(() => {
        getCarsList();
    }, [flag]);

    async function getCarsList() {

        const response = await fetch('https://localhost:7045/api/car/listcars');
        const data = await response.json();
        setCarsList(data);

    }

    function listing() {
        var result = new Array();

        result = result.concat(carsList.map(car => {
            return (
                <tr key={car.id}>
                    <td>{car.id}</td>
                    <td>{car.brand}</td>
                    <td>{car.model}</td>
                    <td>{car.dailyPrice}</td>
                </tr>
            );
        }));
        return result;
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
                    {listing()}
                </tbody>
            </table>
        </div>
    );

}

export default Cars;
