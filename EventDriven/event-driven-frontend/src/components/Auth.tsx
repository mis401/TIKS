import React, { useState } from "react";
import '../styles/Auth.css'
import { useNavigate } from "react-router-dom";
//import { User } from '../redux/authTypes'
import { useDispatch } from 'react-redux';
import { loginSuccess } from "../redux/authReducer";

function Auth(){
    const navigate = useNavigate();
    const dispatch = useDispatch();
    
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });

    const handleFieldChange = (fieldName: string, value: string) => {
        setFormData((prevData) => ({
            ...prevData,
            [fieldName]: value,
        }));
    };
    
    const handleSignIn = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        event.preventDefault(); 

        try {
            const response = await fetch('http://localhost:8000/auth/signin', {
                method:'POST',
                headers:{
                    'Content-Type':'application/json',
                },
                body: JSON.stringify({
                    email: formData.email,
                    password: formData.password,
                }),
            });

            if(response.ok) {
                const userData = await response.json();
                console.log('UserData after successful login:', userData);
                console.log(userData)
                dispatch(loginSuccess(userData.user));
                console.log('User data dispatched:', userData);
                navigate('/home');
                
            } else {
                const errorData = await response.json();

                console.error('Signin failed: ', errorData.message);
            }
        }
        catch (error) {
            console.error('Fetch error:', error);
        }
    }
    
    return(
        <div className="authContainer">
        <div className="signInContainer">
            <form name="signinForm" className="form">
            <h1 className="title">Sign In</h1>
                <input type="email" placeholder="Email" className="input" onChange={(e) => handleFieldChange('email', e.target.value)} />
                <input type="name" placeholder="Name" className="input"/>
                <input type="surname" placeholder="Surname" className="input"/>
                
                <button className="button" onClick={(e) => handleSignIn(e)}>Sign In</button>
            </form>
        </div> 
    </div>
);
}

export default Auth;