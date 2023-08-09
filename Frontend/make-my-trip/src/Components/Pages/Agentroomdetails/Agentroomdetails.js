import React, { useState, useEffect } from 'react';
import './Agentroomdetails.css';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { Carousel } from 'react-responsive-carousel';
import { useParams } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { BlobServiceClient } from '@azure/storage-blob';

const Agentroomdetails = () => {
  var { id } = useParams();

  const [amenities, setAmenities] = useState([]);
  const [newAmenity, setNewAmenity] = useState('');
  const [images, setImages] = useState([]);
  const [roomsData, setRoomsData] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const roomsPerPage = 5;
  const [hotel, setHotel] = useState(null);
  const [hotelImage, setHotelImage] = useState({
    "id": 0,
    "hotelId" : Number(id),
    "amenityTypeOrImage": ""
  })

  const [showRoomPopup, setShowRoomPopup] = useState(false);

  const totalPages = Math.ceil(roomsData.length / roomsPerPage);

  const handleDeleteAmenity = (index) => {
    const updatedAmenities = amenities.filter((_, i) => i !== index);
    setAmenities(updatedAmenities);
    toast.success('Amenity deleted successfully.');
  };
  const [imageFile, setImageFiles] = useState();
  const handleImageChange = (event) => {
    const files = Array.from(event.target.files);
    setImageFiles(files);
    const str ="http://127.0.0.1:10000/devstoreaccount1/bigbang/bigbang/"+files[0].name
    setHotelImage({...hotelImage,amenityTypeOrImage:str})
  };

  const handleUpload = async () => {
    const AZURITE_BLOB_SERVICE_URL = 'http://localhost:10000';
    const ACCOUNT_NAME = 'devstoreaccount1';
    const ACCOUNT_KEY = 'Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==';
  
    const blobServiceClient = new BlobServiceClient(
      "http://127.0.0.1:10000/devstoreaccount1/bigbang?sv=2018-03-28&st=2023-08-09T04%3A46%3A49Z&se=2023-08-10T04%3A46%3A49Z&sr=c&sp=racwdl&sig=fhVDk6bpjm2WQsxam7UvsGSufUPE%2BQ5jruRnPKy%2Fxb8%3D",
      "sv=2018-03-28&st=2023-08-09T04%3A46%3A49Z&se=2023-08-10T04%3A46%3A49Z&sr=c&sp=racwdl&sig=fhVDk6bpjm2WQsxam7UvsGSufUPE%2BQ5jruRnPKy%2Fxb8%3D"
    );
  
    const containerClient = blobServiceClient.getContainerClient('bigbang');
        const blobClient = containerClient.getBlobClient(imageFile[0].name);
        const blockBlobClient = blobClient.getBlockBlobClient();
        const result = await blockBlobClient.uploadBrowserData(imageFile[0], {
          blockSize: 4 * 1024 * 1024,
          concurrency: 20,
          onProgress: ev => console.log(ev)
        });
  };

  const addAmenity = async () => {
    if (newAmenity) {
      try {
        const response = await fetch(`http://localhost:5252/api/Amenity/AddAmenityInformation`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            hotelId: id,
            amenityTypeOrImage: encodeURIComponent(newAmenity),
          }),
        });

        if (response.ok) {
          const newAmenityData = [...amenities, { amenityTypeOrImage: newAmenity }];
          setAmenities(newAmenityData);
          closeAmenityPopup();
          toast.success('Amenity added successfully.');
        } else {
          console.log('Failed to add amenity. Status:', response.status);
          const errorResponseText = await response.text();
          console.log('Error Response:', errorResponseText);
          toast.error('Failed to add amenity.');
        }

      } catch (error) {
        console.error(error);
      }
    }
  };

  const uploadImage = async () => {
    handleUpload(); {
      try {
        const response = await fetch(`http://localhost:5252/api/Image/AddImage`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(hotelImage),
        });
  
        if (response.ok) {
          closeImagePopup();
          toast.success('Image uploaded successfully.');
        } else {
          toast.error('Failed to upload image.');
        }
      } catch (error) {
        console.error('Error uploading image:', error);
      }
    }
  };
  

  var agent = Number(sessionStorage.getItem('userId'));
  console.log(agent);

  const [roomInputs, setRoomInputs] = useState({
    hotelId: id,
    agentId: agent,
    roomId: '',
    price: '',
    capacity: '',
    roomType: '',
  });

  const addRoom = async () => {
    if (
      roomInputs.price &&
      roomInputs.capacity &&
      roomInputs.roomType
    ) {
      try {
        const response = await fetch(`http://localhost:5252/api/Room/AddRoomInformation`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            hotelId: id,
            roomId: 0,
            price: roomInputs.price,
            capacity: roomInputs.capacity,
            roomType: roomInputs.roomType,
          }),
        });

        if (response.ok) {
          const jsonData = await response.json();
          setRoomsData([...roomsData, jsonData]);
          closeRoomPopup();
          toast.success('Room added successfully.');
        } else {
          toast.error('Failed to add room.');
        }

      } catch (error) {
        console.error('Error adding room:', error);
      }
    }
  };

  const openAmenityPopup = () => {
    setShowAmenityPopup(true);
  };

  const closeAmenityPopup = () => {
    setShowAmenityPopup(false);
    setNewAmenity('');
  };

  const handleAmenityInputChange = (e) => {
    setNewAmenity(e.target.value);
  };

  const [showImagePopup, setShowImagePopup] = useState(false);
  const [selectedImage, setSelectedImage] = useState(null);

  const openImagePopup = () => {
    setShowImagePopup(true);
  };

  const closeImagePopup = () => {
    setShowImagePopup(false);
    setSelectedImage(null);
  };

  const [showAmenityPopup, setShowAmenityPopup] = useState(false);

  const openRoomPopup = () => {
    setShowRoomPopup(true);
  };

  const closeRoomPopup = () => {
    setShowRoomPopup(false);
    setRoomInputs({
      roomId: '',
      price: '',
      capacity: '',
      roomType: '',
    });
  };

  const handleRoomInputChange = (e) => {
    const { name, value } = e.target;
    setRoomInputs((prevInputs) => ({
      ...prevInputs,
      [name]: value,
    }));
  };

  useEffect(() => {
    if (id) {
      const fetchRoomsData = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Room/FetchRoomsByHotelId`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setRoomsData(jsonData);
        } catch (error) {
          console.error('Error fetching room details:', error);
        }
      };

      fetchRoomsData();
    }
  }, [id]);

  useEffect(() => {
    if (id) {
      const fetchHotelDetails = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Hotel/FetchHotelById`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setHotel(jsonData);
        } catch (error) {
          console.error('Error fetching hotel details:', error);
        }
      };

      fetchHotelDetails();
    }
  }, [id]);

  useEffect(() => {
    if (id) {
      const fetchAmenities = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Amenity/FetchAmenitiesByHotelId`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setAmenities(jsonData);
        } catch (error) {
          console.error('Error fetching amenities:', error);
        }
      };

      fetchAmenities();
    }
  }, [id]);


  const [hId,setHId]=useState(
    {
      "hotelId": 0
    }
  )

  useEffect(()=>{
    hId.hotelId=id
    fetch("http://localhost:5252/api/Image/FetchImagesByHotelId",
            {
                "method": "POST",
                headers: {
                    "accept": "text/plain",
                    "Content-Type": 'application/json',
                },
                "body": JSON.stringify(hId)
            })
            .then(async (data) => {
                if (data.status == 200) {
                    var myData = await data.json();
                    console.log(myData, "output");
                    setImages(myData);
                }
                else {
                    console.log(await data.text());
                }
            })
            .catch((err) => {
                console.log(err.error)
            })
  },[])

  if (!hotel) {
    return <div>Loading...</div>;
  }

  return (
    <div className="hotel-details-container container-hotel">
      <div className="left-side">
        <div className="carousel-room">
          <Carousel>
            {images.map((image, index) => (
              <div key={index}>
                <img src={image.amenityTypeOrImage} alt={`Image ${index + 1}`} />
              </div>
            ))}
          </Carousel>
        </div>

        <div className="hotel-location">
          <p>Country: {hotel.country}</p>
          <p>City: {hotel.city}</p>
          <p>Number of Rooms: {hotel.numberOfRooms}</p>
          <p>Contact: {hotel.contactNumber}</p>
          <p>Email: {hotel.email}</p>
          <p>Price Range:   &#x20B9;{hotel.minimumPriceRange}-&#x20B9;{hotel.maximumPriceRange}</p>
        </div>
      </div>

      <div className="right-side">
        <div className="hotel-name">
          <span>
            <h2>
              {hotel.name}
            </h2>
          </span>
        </div>

        <div className="button-row">
          <button onClick={openImagePopup} className="btn">
            Upload Image
          </button>
          <button onClick={openAmenityPopup} className="btn">
            Add Amenity
          </button>
          <button onClick={openRoomPopup} className="btn">
            Add Room
          </button>
      </div>


        <div className="amenities">
          <h2>Amenities</h2>
          <div className="amenities-container">
            {amenities.map((amenity, index) => (
              <div key={index} className="amenity-item">
                <span>{amenity.amenityTypeOrImage}</span>
                <button
                  onClick={() => handleDeleteAmenity(index)}
                  className="delete-amenity"
                >
                  &#10060;
                </button>
              </div>
            ))}
          </div>
        </div>

        

        <div className="rooms">
          <h2>Rooms Available</h2>
          <table>
            <thead>
              <tr>
                <th>Room ID</th>
                <th>Type</th>
                <th>Price</th>
                <th>Capacity</th>
              </tr>
            </thead>
            <tbody>
              {roomsData
                .slice((currentPage - 1) * roomsPerPage, currentPage * roomsPerPage)
                .map((room) => (
                  <tr key={room.id}>
                    <td>{room.id}</td>
                    <td>{room.roomType}</td>
                    <td>{room.price}</td>
                    <td>{room.capacity}</td>
                  </tr>
                ))}
            </tbody>
          </table>
          <div className="pagination">
            <button
              className="btn"
              onClick={() => setCurrentPage(currentPage - 1)}
              disabled={currentPage === 1}
            >
              Previous
            </button>
            <button
              className="btn"
              onClick={() => setCurrentPage(currentPage + 1)}
              disabled={currentPage === totalPages}
            >
              Next
            </button>
          </div>
        </div>
        <ToastContainer />
      </div>

      {showAmenityPopup && (
        <div className="popup-overlay">
          <div className="popup-content">
            <h2>Add Amenity</h2>
            <input
              type="text"
              placeholder="Amenity Name"
              value={newAmenity}
              onChange={handleAmenityInputChange}
              className="popup-input"
            />
            <button onClick={addAmenity} className="popup-button">
              Add
            </button>
            <button onClick={closeAmenityPopup} className="popup-button btn-secondary">
              Cancel
            </button>
          </div>
        </div>
      )}

      {showRoomPopup && (
        <div className="popup-overlay">
          <div className="popup-content">
            <h2>Add Room</h2>
            <label>Price</label>
            <input
              type="text"
              placeholder="Price"
              name="price"
              value={roomInputs.price}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
            <label>Capacity</label>
            <input
              type="text"
              placeholder="Capacity"
              name="capacity"
              value={roomInputs.capacity}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
            <label>Room Type</label>
            <input
              type="text"
              placeholder="Room Type"
              name="roomType"
              value={roomInputs.roomType}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
            <button onClick={addRoom} className="popup-button">
              Add
            </button>
            <button onClick={closeRoomPopup} className="popup-button btn-secondary">
              Cancel
            </button>
          </div>
        </div>
      )}

      {showImagePopup && (
        <div className="popup-overlay">
          <div className="popup-content">
            <h2>Upload Image</h2>
            <input
              type="file"
              accept="image/*"
              onChange={handleImageChange}
              className="popup-input"
            />
            <button onClick={uploadImage} className="popup-button">
              Upload
            </button>
            <button onClick={closeImagePopup} className="popup-button btn-secondary">
              Cancel
            </button>
          </div>
        </div>
      )}

    </div>
  );
};

export default Agentroomdetails;
