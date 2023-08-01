import React from "react";
import "./Portfolio.css";

//Imported Assets
import icon1 from './icon1.png';
import icon2 from './icon2.png';
import icon3 from './icon3.png';
import icon4 from './icon3.jpg';

const Portfolio = () => {
  return (
    <div className="portfolio section container">
      <div className="secContainer grid-port">
        <div className="leftContent">
          <div className="secHeading">
            <h3 className="h3">Why Should You Choose Us</h3>
            <p className="p">
              We have extensive knowledge and experience in the travel industry
            </p>
          </div>

          <div className="grid-port">
            <div className="singlePortfolio flex">
              <div className="iconDiv">
                <img className="icon-icon" src={icon1} alt="Icon image" />
              </div>
              <div className="infor">
                <h4 className="h4">Safety and support</h4>
                <p className="p">
                  Our top priority is the safety and well-being of our clients.
                  We maintain high safety standards and have emergency support
                  available during the trip.
                </p>
              </div>
            </div>
          </div>

          <div className="grid-port">
                        <div className="singlePortfolio flex">
                            <div className="iconDiv">
                                <img className="icon-icon" src={icon2} alt="Icon image" />
                            </div>
                            <div className="infor">
                                <h4 className=".h4">Diverse Range Of Destinations</h4>
                                <p  className=".p">
                                    Whether it's a domestic tour or an
                                    international adventure, we cover a wide
                                    range of destinations to cater to different
                                    interests and preferences.
                                </p>
                            </div>
                        </div>
                    </div>

                    <div className="grid-port">
                        <div className="singlePortfolio flex">
                            <div className="iconDiv">
                                <img className="icon-icon" src={icon3} alt="Icon image" />
                            </div>
                            <div className="infor">
                                <h4 className=".h4">24/7 Customer Support</h4>
                                <p className=".p">
                                    Our dedicated customer support team is available
                                    round the clock to address any queries or concerns
                                    before, during and after the trip.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
        <div className="rightContent">
          <img className="icon-icon" src={icon4} alt=" " />
        </div>
      </div>
    </div>
  );
};

export default Portfolio;

