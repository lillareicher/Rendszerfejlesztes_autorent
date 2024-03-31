import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './cardetails.css';

function CarDetails() {
    const  params  = useParams();
    const { carId } = params;
    return (
        <div>
            <h3>Car details of car Id: {carId} shown here.</h3>
            EZEK CSAK MINTA ADATOK:
            <div>
                About this car:
                <table border="1">
                    <tbody>
                        <tr>
                            <td >Car Id</td>
                            <td >Brand</td>
                            <td >Model</td>
                            <td >Category</td>
                            <td >Daily Price</td>
                        </tr>
                        <tr>
                            <td >c1</td>
                            <td >Toyota</td>
                            <td >Carmy</td>
                            <td >Five-seat</td>
                            <td >50$</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <br></br>
            <div>
                Reserved on these dates:
                <table border="1">
                    <tbody>
                        <tr>
                            <td >From</td>
                            <td >To</td>
                        </tr>
                        <tr>
                            <td >04.01</td>
                            <td >04.05</td>
                        </tr>
                        <tr>
                            <td >04.10</td>
                            <td >04.12</td>
                        </tr>
                    </tbody>
                </table>

            </div>

            <div>
            Make a reservation here:
                <br></br>
                <label>Start date:</label> <input name="startDate" type="date"></input>
                <br></br>
                <label>End date:</label> <input name="endDate" type="date"></input>
                <br></br>
                <button>Reserve</button>
            </div>

        </div>
    );
}

export default CarDetails;