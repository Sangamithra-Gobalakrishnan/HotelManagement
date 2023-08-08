import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Aos from 'aos';
import 'aos/dist/aos.css';
import { MdOutlineTravelExplore } from "react-icons/md";
import { AiFillCloseCircle } from "react-icons/ai";
import { TbGridDots } from "react-icons/tb";
import { AiOutlineUserAdd } from 'react-icons/ai';
import { BsFillTelephoneOutboundFill } from 'react-icons/bs';
import { HiOutlineLocationMarker } from 'react-icons/hi';
import { MdOutlineMarkEmailRead } from 'react-icons/md';
import './Adminpage.css';

const Adminpage = () => {

    const [agents, setAgents] = useState([]);

    const fetchData = async () => {
      try {
        const response = await fetch('http://localhost:5104/api/Filter/GetNotApprovedAgents');
        const jsonData = await response.json();
        setAgents(jsonData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    useEffect(() => {
      fetchData();
    }, []);

    const updateStatus = async (id) => {
      try {
        await fetch(`http://localhost:5104/api/UserAuthentication/ApproveAgent`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ agentId: id }),
        });
  
        const updatedagents = agents.map((agent) => {
          if (agent.id === id) {
            return {
              ...agent,
              user: {
                ...agent.user,
              },
            };
          }
          return agent;
        });
        setAgents(updatedagents);
        fetchData();
      } catch (error) {
        console.error('Error updating status:', error);
      }
    };

    //Scroll animation

    useEffect(()=>{
      Aos.init({duration:2000})
    },[])


    const navigate = useNavigate();
    const [active, setActive] = useState(true); // Keep the navbar active by default

    const handleLogout = () => {
        sessionStorage.clear();
        navigate('/');
    };

    useEffect(() => {
        setActive(true); // Keep the navbar active initially
    }, []);

    // Function to toggle Navbar
    const toggleNavbar = () => {
        setActive(prevActive => !prevActive);
    }

    return (
        <section className="navBarSection">
            <header className="header flex">
                <div className="logoDiv">
                    <a href="#" className="logo flex">
                        <h1><MdOutlineTravelExplore className="icon"/> Mithra Majesty.</h1>
                    </a>
                </div>

                <div className={`navBar ${active ? 'activeNavbar' : ''}`}>
                    <ul className="navLists flex">
                        <li className="navItem">
                            <a href="/approveAgents" className="navLink">Approve Agents</a>
                        </li>
                        <li className="navItem">
                            <a href="/approvedAgents" className="navLink">Approved Agents</a>
                        </li>
                        <button className="btn">
                            <a href="/" onClick={handleLogout}>LOGOUT</a>
                        </button>
                    </ul>

                    <div onClick={toggleNavbar} className="closeNavbar">
                        <AiFillCloseCircle className="icon"/>
                    </div>
                </div>

                <div onClick={toggleNavbar} className="toggleNavbar">
                    <TbGridDots className="icon"/>
                </div>
            </header>
            <section className='main container-hotel section'>
           <div className='secTitle'>
             <h3 className='title'>
               Approve Agents  
             </h3>
           </div>

           <div className='secContent grid'>
              {agents.map((agent) => (
                <div key={agent.id}
                className='singleDestination'>
                  
                  <div className='imageDiv'>
                  <AiOutlineUserAdd className = "imgcs1"/>
                  </div>

                  <div className='cardInfo'>
                     <h4 className='desTitle'>{agent.user.username}</h4>
                     <span className='continent flex'>
                     <HiOutlineLocationMarker className='icon' />
                      <span className='name'>
                        {agent.city}&nbsp;,&nbsp;{agent.state}&nbsp;,&nbsp;{agent.country}
                      </span>
                     </span>

                     <div className='price'>
                        <h5>
                        <MdOutlineMarkEmailRead />&nbsp;&nbsp;
                        {agent.user.emailId} 
                        </h5>
                      </div>

                     <div className='fees flex'>
                      <div className='grade'>
                        <span>Age&nbsp;&nbsp;<small className='small'>{agent.age}</small></span>
                      </div>

                      <div className='desc'>
                       <p> <BsFillTelephoneOutboundFill />&nbsp;&nbsp;{agent.user.phoneNumber}</p>
                     </div>

                     </div>

                     <button className='btn flex'
                       onClick={() => updateStatus(agent.id)}
                       >
                      APPROVE <AiOutlineUserAdd className = "icon"/>
                     </button>

                  </div>
                </div>
              ))} 
           </div>
        </section>
        </section>
    )
}

export default Adminpage;
