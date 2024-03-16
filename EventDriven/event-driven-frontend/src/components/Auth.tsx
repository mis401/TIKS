import React, { useState } from "react";
import '../styles/Auth.css'
import { useNavigate } from "react-router-dom";
import { User } from '../redux/authTypes'
import { useDispatch } from 'react-redux';
import { loginSuccess } from "../redux/authReducer";

function Auth(){
    const [signIn, setSignIn] = useState<boolean>(true);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    
    const handleSignInClick = () => setSignIn(false);
    const handleSignUpClick = () => setSignIn(true);

    const [formData, setFormData] = useState({
        name: '',
        surname: '',
        email: '',
        password: '',
        confirmPassword: '',
    });

    const setUser = (userData: User) => ({
        type: 'LOGIN_SUCCESS',
        payload: userData,
      });

    const handleFieldChange = (fieldName: string, value: string) => {
        setFormData((prevData) => ({
            ...prevData,
            [fieldName]: value,
        }));
    };
    
    const handleSignUp = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        event.preventDefault(); 
    
        if(formData.password !== formData.confirmPassword){
            console.error('Passwords do not match');
            return;
        }
        try {
            const response = await fetch('http://localhost:5019/user/create', {
                method:'POST',
                headers:{
                    'Content-Type':'application/json',
                },
                body: JSON.stringify({
                    name: formData.name,
                    surname: formData.surname,
                    email: formData.email,
                    password: formData.password,
                }),
            });

            if(response.ok) {
                handleSignInClick();
            } else {
                const errorData = await response.json();

                console.error('Signup failed: ', errorData.message);
            }
        }
        catch (error) {
            console.error('Fetch error:', error);
        }
    }

    const handleSignIn = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        event.preventDefault(); 

        try {
            const response = await fetch(`http://localhost:5019/user/login`, {
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
    
                dispatch(loginSuccess(userData));
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
            <div className={`signUpContainer ${signIn ? 'signUpContainer-nonSignedIn' : ''}`}>
                <form name="signupForm" className="form">
                <h1 className="title">Create Account</h1>
                    <input type='text' placeholder='First Name' className="input" onChange={(e) => handleFieldChange('name', e.target.value)}/>
                    <input type='text' placeholder='Last Name' className="input" onChange={(e) => handleFieldChange('surname', e.target.value)} /> 
                    <input type='email' placeholder='Email' className="input" onChange={(e) => handleFieldChange('email', e.target.value)} />
                    <input type='password' placeholder='Password' className="input" onChange={(e) => handleFieldChange('password', e.target.value)} />
                    <input type='password' placeholder='Confirm Password' className="input"onChange={(e) => handleFieldChange('confirmPassword', e.target.value)}  />
                    <button className="button" onClick={(e) => handleSignUp(e)}>Sign Up</button>
                </form>
            </div>  
            <div className={`signInContainer ${signIn ? ' signInContainer-nonSignedIn' : ''}`}>
                <form name="signinForm" className="form">
                <h1 className="title">Sign In</h1>
                    <input type="email" placeholder="Email" className="input" onChange={(e) => handleFieldChange('email', e.target.value)} />
                    <input type="password" placeholder="Password" className="input" onChange={(e) => handleFieldChange('password', e.target.value)} />
                    <a href="#" className="anchor">Forgot your password?</a>
                    <button className="button" onClick={(e) => handleSignIn(e)}>Sign In</button>
                </form>
            </div> 
            <div className={`overlayContainer ${signIn ? ' overlayContainer-nonSignedIn' : ''}`}>
                    <div className={`overlay ${signIn ? ' overlay-nonSignedIn' : ''}`}>
                        <div className={`leftOverlayPanel ${signIn ? ' leftOverlayPanel-nonSignedIn' : ''}`}>
                            <h1 className="title">Welcome Back!</h1>
                            <p className="paragraph">
                                To keep connected with us please login with your personal info
                            </p>
                            <button className="ghostButton" onClick={handleSignInClick}>Sign In</button>
                        </div>

                        <div className={`rightOverlayPanel ${signIn ? ' rightOverlayPanel-nonSignedIn' : ''}`}>
                            <h1 className="title">Hello, Friend!</h1>
                            <p className="paragraph">
                                Enter Your personal details and start the journey with us
                            </p>
                            <button className="ghostButton" onClick={handleSignUpClick}>Sign Up</button>
                        </div> 
                    </div>
                </div>
        </div>
);
}

export default Auth;