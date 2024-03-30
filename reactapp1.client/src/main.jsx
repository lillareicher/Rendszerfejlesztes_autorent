import React from 'react'
import ReactDOM from 'react-dom/client'
import Login from './Login.jsx'
import Cars from './Cars.jsx'
import {
    createBrowserRouter,
    RouterProvider,
    //Route
} from "react-router-dom";
import './index.css'

const router = createBrowserRouter([
    {
        path: "",
        element: <Login />
    },
    {
        path: "cars",
        element: <Cars />
    }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>,
)



