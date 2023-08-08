import React, { useState, useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import '@fortawesome/fontawesome-free/css/all.css';
import './Login.css';
import log from './log.svg';
import reg from './register.svg';
import { useNavigate } from 'react-router-dom';

function Login() {

  const navigate = useNavigate();
  const [isBusinessUser, setIsBusinessUser] = useState(false);

  const [user, setUser] = useState({
    "id": 0,
    "userId": 0,
      "user": {
        "id":0,
        "name": "",
        "username": "",
        "emailId": "",
        "phoneNumber": ""
      },
      "dateOfBirth": "",
      "city": "",
      "state": "",
      "country": "",
      "password": ""
  });

  const [users,setUsers] = useState({
    "phoneNumber": "",
    "password": "",
    "userId": 0, 
    "role": "", 
    "token": "",
    "status": ""
  });

  const handleLogin = async (e) => {
    e.preventDefault();


    const endpointlogin = 'http://localhost:5104/api/UserAuthentication/Login'; 
    

    try {
      console.log(users);
      const response = await fetch(endpointlogin, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(users)
      });  

      if (response.ok) {
        const responseData = await response.json(); 
        console.log(responseData);
          if (responseData !== "") {
            sessionStorage.setItem("userId", responseData.userId);
            sessionStorage.setItem("role", responseData.role);
            sessionStorage.setItem("token", responseData.token);
            sessionStorage.setItem("status", responseData.status);
        }
        if(responseData.role == "admin"){
          navigate("/adminPage");
        }
        else if (responseData.role == "traveller") {
           navigate("/travellerPage")
        } else {
          navigate("/agentPage");
        }
      } else {
        const data = await response.json();
        toast.error(data.message || 'Invalid credentials!');
      }
    } catch (error) {
      toast.error('An error occurred. Please try again later.');
    }
  };

  const handleUserTypeChange = () => {
    setIsBusinessUser((prev) => !prev);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

   const endpoint = isBusinessUser
   ? 'http://localhost:5104/api/UserAuthentication/TravelAgentRegistration'
   : 'http://localhost:5104/api/UserAuthentication/TravellerRegistration';

    try {
      console.log(user);
      const response = await fetch(endpoint, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)
      });  

      if (response.ok) {
        const responseData = await response.json(); 
        if (responseData.status === 200) {
          if (responseData.token !== "") {
            localStorage.setItem("token", responseData.token);
          }
        }
        navigate("/home");
      } else {
        const data = await response.json();
        toast.error(data.message || 'Registration failed. Please try again.');
      }
    } catch (error) {
      toast.error('An error occurred. Please try again later.');
    }
  };

  useEffect(() => {
    const sign_in_btn = document.querySelector("#sign-in-btn");
    const sign_up_btn = document.querySelector("#sign-up-btn");
    const container = document.querySelector(".container");

    if (container) {
      sign_up_btn.addEventListener("click", () => {
        container.classList.add("sign-up-mode");
      });

      sign_in_btn.addEventListener("click", () => {
        container.classList.remove("sign-up-mode");
      });
    }
    return () => {
      if (container) {
        sign_up_btn.removeEventListener("click", () => {});
        sign_in_btn.removeEventListener("click", () => {});
      }
    };
  }, []);

  return (
    <div className="container">
      <div className="forms-container">
        <div className="signin-signup">
          <form action="#" className="sign-in-form" onSubmit={handleLogin}>
          <h2 className="title">SIGN IN</h2>
          <div className="input-field">
          <i className="fas fa-phone"></i>
          <input
            type="text"
            placeholder="Phone Number"
            name="phoneNumber"
            onChange={(event) => {
              setUsers({ ...users, phoneNumber:event.target.value });
            }}
          />
        </div>
        <div className="input-field">
          <i className="fas fa-lock"></i>
          <input
            type="password"
            placeholder="Password"
            name="password"
            onChange={(event) => {
              setUsers({ ...users, password:event.target.value });
            }}
          />
        </div>
            <input type="submit" value="Sign In" className="btn solid" />
            <p className="social-text">Or Sign in with social platforms</p>
            <div className="social-media">
              <a href="#" className="social-icon">
                <i className="fab fa-facebook-f"></i>
              </a>
              <a href="#" className="social-icon">
                <i className="fab fa-twitter"></i>
              </a>
              <a href="#" className="social-icon">
                <i className="fab fa-google"></i>
              </a>
              <a href="#" className="social-icon">
                <i className="fab fa-linkedin-in"></i>
              </a>
            </div>
          </form>
          <form
            action="#"
            className={`sign-up-form ${isBusinessUser ? "business-user" : ""}`}
            onSubmit={handleSubmit}
          >
            <h2 className="title">SIGN UP</h2>
            <div className="input-field">
              <i className="fas fa-user"></i>
              <input
                  type="text"
                  placeholder="Username"
                  name="username"
                  onChange={(event) => {
                    setUser((prevUser) => ({
                      ...prevUser,
                      user: {
                        ...prevUser.user,
                        username: event.target.value,
                      },
                    }));
                  }}
                />
            </div>
            <div className="input-field">
              <i className="fas fa-user"></i>
              <input
                type="text"
                placeholder="Name"
                name="name"
                onChange={(event) => {
                  setUser((prevUser) => ({
                    ...prevUser,
                    user: {
                      ...prevUser.user,
                      name: event.target.value,
                    },
                  }));
                }}
              />
            </div>
            <div className="input-field">
              <i className="fas fa-envelope"></i>
              <input
                type="email"
                placeholder="Email"
                name="email"
                onChange={(event) => {
                  setUser((prevUser) => ({
                    ...prevUser,
                    user: {
                      ...prevUser.user,
                      emailId: event.target.value,
                    },
                  }));
                }}
              />
            </div>
            <div className="input-field">
              <i className="fas fa-phone"></i>
              <input
                type="text"
                placeholder="Phone Number"
                name="phoneNumber"
                onChange={(event) => {
                  setUser((prevUser) => ({
                    ...prevUser,
                    user: {
                      ...prevUser.user,
                      phoneNumber: event.target.value,
                    },
                  }));
                }}
              />
            </div>
          <div className="input-field">
            <i className="fas fa-location-dot"></i>
            <input
              type="text"
              placeholder="Country,State,City"
              name="location"
              onChange={(event) => {
                const locationParts = event.target.value.split(',');
                setUser(prevUser => ({
                  ...prevUser,
                  country: locationParts[0] || "",
                  state: locationParts[1] || "",
                  city: locationParts[2] || "",
                }));
              }}
            />
          </div>
          <div className="input-field">
            <i className="fas fa-calendar"></i>
            <input
              className='dateClr'
              type="date"
              placeholder="DateOfBirth"
              name="dateOfBirth"
              onChange={(event) => {
                setUser({ ...user, dateOfBirth:event.target.value });
              }}
            />
          </div>
          <div className="input-field">
            <i className="fas fa-lock"></i>
            <input
              type="password"
              placeholder="Password"
              name="password"
              onChange={(event) => {
                setUser({ ...user, password:event.target.value });
              }}
            />
          </div>
            <div className="input-field1">
              <input
                className="check-box"
                type="checkbox"
                id="userType"
                onChange={handleUserTypeChange}
                checked={isBusinessUser}
              />
              <label htmlFor="userType">Register as Travel Agent</label>
            </div>
            <input type="submit" className="btn" value="Sign up" />
            <p className="social-text">Or Sign up with social platforms</p>
            <div className="social-media">
              <a href="#" className="social-icon">
                <i className="fab fa-facebook-f"></i>
              </a>
              <a href="#" className="social-icon">
                <i className="fab fa-twitter"></i>
              </a>
              <a href="#" className="social-icon">
                <i className="fab fa-google"></i>
              </a>
              <a href="#" className="social-icon">
                <i className="fab fa-linkedin-in"></i>
              </a>
            </div>
          </form>
        </div>
      </div>

      <div className="panels-container">
        <div className="panel left-panel">
          <div className="content">
            <h3>Welcome to Mithra Majesty.</h3>
            <p>
              Join us and experience luxurious stays, impeccable service, and
              unforgettable memories. Sign up now to unlock exclusive offers and
              rewards.
            </p>
            <button className="btn-login transparent" id="sign-up-btn">
              Sign Up
            </button>
          </div>
          <img src={log} className="image" alt="Hotel Lobby" />
        </div>

        <div className="panel right-panel">
          <div className="content">
            <h3>Welcome back to Mithra Majesty.</h3>
            <p>
              Enter your credentials to access your account and manage your bookings.
              Your next extraordinary adventure awaits!
            </p>
            <button className="btn-login transparent" id="sign-in-btn">
              Sign In
            </button>
          </div>
          <img src={reg} className="image" alt="" />
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}

export default Login;
