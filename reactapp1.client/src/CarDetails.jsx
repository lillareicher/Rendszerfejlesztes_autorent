import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

function CarDetails() {
    const  params  = useParams();
    const { carId } = params;
    return (
        <div>
            <h3>Car Details of {carId} shown here.</h3>
        </div>
    );
}

export default CarDetails;