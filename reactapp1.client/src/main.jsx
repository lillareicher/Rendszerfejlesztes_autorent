import React from 'react'
import ReactDOM from 'react-dom/client'
import Login from './Login.jsx'
import Cars from './Cars.jsx'
import User from './User.jsx'

import CarDetails from './CarDetails';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import './index.css'

const router = createBrowserRouter([
    {
        path: "",
        element: <Login />
    },
    {
        path: "/:username/cars",
        element: <Cars />
    },
    {
        path: "/:username/cars/:carId",
        element: <CarDetails />
    },
    {
        path: "/:username",
        element: <User />
    }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>,
)

