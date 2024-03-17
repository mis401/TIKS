import React, { useEffect, useState } from 'react';
import { connect, ConnectedProps, useSelector } from 'react-redux';
//import { RootState } from '../redux/rootReducer';
//import { User } from '../redux/authTypes';
import SimpleDialog, { SimpleDialogProps } from './SimpleDialog';
import { useDispatch } from 'react-redux';
import { loginSuccess } from "../redux/authReducer";
import { useNavigate } from "react-router-dom";


export const Sidebar: React.FC = () => {
  const dispatch = useDispatch();

  const [openDialog, setOpenDialog] = React.useState(false);
  const [selectedOption, setSelectedOption] = React.useState<string | null>(null);
  const userInState = useSelector((state: any) => state.auth.user);
  const [communities, setCommunities] = React.useState<any[]>([]);
  const [selectedValue, setSelectedValue] = useState<string>(""); 
  const [formData, setFormData] = useState({
    name: '',
    userId: userInState.id
});

  useEffect(() => {
    if (userInState !== null) {
      console.log('User prop changed:', userInState);
      const response = fetch(`http://localhost:5019/community/get-all?userId=${userInState.id}`, {
        method: `GET`
      })
      response.then(async (value) => {
        if (value.ok){
          const data = value.json();
          console.log(data);
          data.then((array) => {
            setCommunities([...array]);
          })
        }
      })
    }
  }, [userInState]);

 


  const addCommunityClick = () => {
    setOpenDialog(true);
  };

  function handleDialogClose(value: string): void {
    setSelectedOption(value);
  }

  const handleCreateButtonClick = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    event.preventDefault(); 
    try {
        const response = await fetch(`http://localhost:5019/community/create`, {
            method:'POST',
            headers:{
                'Content-Type':'application/json',
            },
            body: JSON.stringify({
                name: formData.name,
                userId: formData.userId,
            }),
        });

        console.log(formData.name, formData.userId)
        if(response.ok) {
            setOpenDialog(false);
        } else {
            const errorData = await response.json();

            console.error('Creation failed: ', errorData.message);
        }
    }
    catch (error) {
        console.error('Fetch error:', error);
    }
}

const handleNameChange = (value: string): void => {
  setFormData({ ...formData, name: value }); 
};

const handleJoinButtonClick = async (): Promise<void> => {
  try {
    const response = await fetch(`http://localhost:5019/community/join?userId=${userInState.id}&communityCode=${formData.name}`, {
      method: 'PUT',
    });

    if (response.ok) {
      console.log(`User ${userInState.name} ${userInState.surname} joined community with code ${selectedValue}`);
      setOpenDialog(false);
    } else {
      const errorData = await response.json();
      console.error('Join failed: ', errorData.message);
    }
  } catch (error) {
    console.error('Fetch error:', error);
  }
};

 
  const dialogProps: SimpleDialogProps = { 
    open: openDialog,
    selectedValue: selectedValue,
    onNameChange: handleNameChange,
    onClose: handleDialogClose,
    selectedOption: selectedOption,
    onCreateButtonClick: handleCreateButtonClick,
    onJoinButtonClick: handleJoinButtonClick,
    title: "Add a new community",
    options: ['Join a community', 'Create a community'],
    createButtonText: "Create",
    joinButtonText: "Join",
    firstInputLabel: " ",
    secondInputLabel: " ",
    firstInputHint: "Create community",
    secondInputHint: "Join community"
  };

  return (
    <aside className="sidebar">
      <div className="profile-section">
        <div className="profile-pic"></div>
        <div className="user-info">
        <h3>{userInState ? `${userInState.name} ${userInState.surname}` : 'Guest'}</h3>
          <p>{userInState ? userInState.email : 'email@example.com'}</p>
        </div>
      </div>
      <div className='scrollable-communities'>
        <div className="communities-section">
          {communities.map((community) => (
            <div key={community.id} className="community">    
              <span>{community.name}</span> 
              <span>{community.code}</span>
          
            </div>
          ))}
        </div>
      </div>
      <div className="add-community" >
        <button onClick={addCommunityClick}>+</button>
      </div>

      <SimpleDialog {...dialogProps} /> {/* Prosledite props objekat SimpleDialog-u */}



    </aside>
  );
};

export default Sidebar;
//export default connector(Sidebar);