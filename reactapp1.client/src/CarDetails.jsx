import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
//import './cardetails.css';

function CarDetails() {
    const [loading, setLoading] = useState(true);
    const [carsList, setCarsList] = useState([]);
    //const [rentsList, setRentsList] = useState([]);
    const [fromDate, setFromDate] = useState([]);
    const [toDate, setToDate] = useState([]);
    const params = useParams();
    const { carId } = params;

    //const flag = null;

    useEffect(() => {

        async function getCarsList() {

            const response = await fetch('https://localhost:7045/api/car/listcars');
            const data = await response.json();
            setCarsList(data);
            setLoading(false);
        }

        //async function getRents() {
        //    const data = {
        //        carId: carId
        //    };

        //    fetch('https://localhost:7045/api/rental/getrentals', {
        //        method: 'POST',
        //        headers: {
        //            'Content-Type': 'application/json',
        //        },
        //        body: JSON.stringify(data),
        //    }).then((response) => {
        //        const res = response.json();
        //        setRentsList(res);
        //    }).catch(error => {
        //        console.log(error);
        //    });
        //}

        getCarsList();
        //getRents();
    }, []);

    function printCar() {
        const currentCar = carsList.find(car => car.id === carId);

        return (
            <tr key={currentCar.id}>
                <td>{currentCar.id}</td>
                <td>{currentCar.brand}</td>
                <td>{currentCar.model}</td>
                <td>{currentCar.categoryId}</td>
                <td>{currentCar.dailyPrice}</td>
            </tr>
        );
    }

    //function listRents() {
    //    var result = new Array();

    //    result = result.concat(rentsList.map(rent => {
    //        return (
    //            <tr key={rent.id}>
    //                <td>{rent.fromDate}</td>
    //                <td>{rent.toDate}</td>
    //            </tr>
    //        );
    //    }));

    //    return result;
    //}

    function fromChange(event) {
        setFromDate(event.target.value);
    }
    function toChange(event) {
        setToDate(event.target.value);
    }

    //function makeReserv() {
    //    const data = {
    //        userId: "John",
    //        carId: carId,
    //        fromDate: fromDate,
    //        toDate: toDate,
    //        created: "2024-03-31"
    //    };
    //    fetch('https://localhost:7045/api/rental/newreservation', {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json',
    //        },
    //        body: JSON.stringify(data),
    //    }).then((response) => {
    //        if (!response.ok) {
    //            throw new Error('Reservation unsuccesful.');
    //        }

    //    }).catch(error => {
    //        console.log(error);
    //    });
    //}

    //async function sendReserv() {
    //el kell helyezni a f�ggv�nyt a reserve gombba!!!
    //const data = {
    //    carId: carId,
    //    fromDate: fromDate,
    //    toDate: toDate
    //};

    //fetch('https://localhost:7045/api/rental/validdate', {
    //    method: 'POST',
    //    headers: {
    //        'Content-Type': 'application/json',
    //    },
    //    body: JSON.stringify(data),
    //}).then((response) => {
    //    if (response) {
    //        //�j k�r�s k�ld�se rent l�trehoz�s�hoz
    //        makeReserv();
    //        //sikeress�g kiir�sa
    //        return (
    //            <h3>Your reservation has been succesful!</h3>
    //        );
    //    } else if (!response) {
    //        return (
    //            <h3>Invalid reservation. Please check the dates.</h3>
    //        );
    //    }
    //}).catch(error => {
    //    console.log(error);
    //});
    //}

    if (loading) {
        return <div>Loading data...</div>
    }

    return (
        <div>

            <div>
                About this car:
                <table border="1">
                    <thead>
                        <tr>
                            <td >Car Id</td>
                            <td >Brand</td>
                            <td >Model</td>
                            <td >Category Id</td>
                            <td >Daily Price</td>
                        </tr>
                    </thead>
                    <tbody>
                        {printCar()}
                    </tbody>
                </table>
            </div>

            <br></br>
            EZEK CSAK MINTA ADATOK:
            <div>
                Reserved on these dates:
                <table border="1">
                    <thead>
                        <tr>
                            <td >From</td>
                            <td >To</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td >04.01</td>
                            <td >04.05</td>
                        </tr>
                        <tr>
                            <td >04.10</td>
                            <td >04.12</td>
                        </tr>
                        {/*listRents() */}
                    </tbody>
                </table>

            </div>

            <div>
                Make a reservation here:
                <br></br>
                <label>Start date:</label> <input name="startDate" type="date" onChange={fromChange}></input>
                <br></br>
                <label>End date:</label> <input name="endDate" type="date" onChange={toChange}></input>
                <br></br>
                <button  >Reserve</button>
                {/*reserveState() */}

            </div>
        </div>
    );
}

export default CarDetails;