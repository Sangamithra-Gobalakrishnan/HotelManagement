import React,{useEffect} from "react";
import './Footer.css';
import {FiChevronRight, FiSend} from "react-icons/fi";
import { MdOutlineTravelExplore } from "react-icons/md";
import { AiFillInstagram, AiFillYoutube, AiOutlineTwitter } from "react-icons/ai";
import {FaTripadvisor} from "react-icons/fa";
import footer from './footer.mp4';

import Aos from 'aos';
import 'aos/dist/aos.css';

const Footer = () => {

     //Scroll animation

    useEffect(()=>{
        Aos.init({duration:2000})
    },[])

   return(
    <section className="footer" id="footer">
     <div className="videoDiv">
     <video className="video" src={footer} muted autoPlay loop type="video/mp4"></video>
     </div>

     <div className="secContent container-hotel">
        <div className="contactDiv flex">
            <div data-aos="fade-up" className="text">
                <small className="small">KEEP IN TOUCH</small>
                <h2 className="h2">Travel with us</h2>
            </div>

            <div className="inputDiv flex">
                <input data-aos="fade-up" className="input" type="text" placeholder="Enter Email Address" />
                <button data-aos="fade-up" className="btn flex" type="submit">
                    SEND <FiSend className="icon"/>
                </button>
            </div>
        </div>

        <div className="footerCard flex">
            <div className="footerIntro flex">
                <div className="logoDiv">
                    <a href="#" className="logo flex">
                       <MdOutlineTravelExplore className="icon"/> Mithra Majesty.
                    </a>
                </div>

                <div data-aos="fade-up" className="footerParagraph">
                    Experience the ultimate convenience in hotel booking with our cutting-edge application. 
                    Discover a world of possibilities as you explore a wide range of hotels tailored to your preferences and budget. 
                    Your journey begins with us – start exploring, start booking, and start creating unforgettable memories.
                </div>

                <div data-aos="fade-up" className="footerSocials flex">
                    <AiOutlineTwitter className="icon"/>
                    <AiFillYoutube className="icon"/>
                    <AiFillInstagram className="icon"/>
                    <FaTripadvisor className="icon"/>
                </div>
            </div>

            <div className="footerLinks grid">
                {/* Group One */}
                <div data-aos="fade-up" data-aos-duration="3000" className="linkGroup">
                    <span className="groupTitle">
                        OUR AGENCY
                    </span>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Services
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Insurance
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Agency
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Payment
                    </li> 
                </div>

                 {/* Group Two */}
                <div data-aos="fade-up" data-aos-duration="4000" className="linkGroup">
                    <span className="groupTitle">
                        PARTNERS
                    </span>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Bookings
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Rentcars
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        HostelWorld
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Trivago
                    </li>
                </div>

                {/* Group Three */}
                <div data-aos="fade-up" data-aos-duration="5000" className="linkGroup">
                    <span className="groupTitle">
                       LAST MINUTE
                    </span>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        London
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        California
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Indonesia
                    </li>

                    <li className="footerList flex">
                        <FiChevronRight className="icon"/>
                        Europe
                    </li>
                </div>
            </div>

            <div className="footerDiv flex">
               <small className="small">BEST PLACE TO BOOK HOSTELS TO YOUR NEEDS</small>
               <small className="small">COPYRIGHTS RESERVED - MITHRA MAJESTY</small>
            </div>
        </div>
     </div>
    </section>
   )
}

export default Footer;
