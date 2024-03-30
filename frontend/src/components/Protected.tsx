import { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';

export default function Protected({ children }: any) { 
    let [isLoggedIn, setIsLoggedIn] = useState(true);
    
    useEffect(() => {
        if(localStorage.getItem('token') === null)
            setIsLoggedIn(false)
    }, [])
    
    if(!isLoggedIn)
        return <Navigate to="/"/>

    return children
}