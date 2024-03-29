import * as React from 'react';
import Button from '@mui/material/Button';
import Avatar from '@mui/material/Avatar';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextSnippetRoundedIcon from '@mui/icons-material/TextSnippetRounded';

export interface SimpleDialogProps {
  open: boolean;
  selectedValue: string;
  onClose: (value: string) => void;
  selectedOption: string | null;
  onCreateButtonClick: (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void;
  onJoinButtonClick: (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void; 
  onNameChange: (value: string) => void;
  title: string;
  options: string[];
  createButtonText: string;
  joinButtonText: string;
  firstInputLabel: string; 
  secondInputLabel: string;
  firstInputHint: string;
  secondInputHint: string;
}


function SimpleDialog(props: SimpleDialogProps) {
  const {
    onClose,
    selectedValue,
    onNameChange,
    open,
    selectedOption,
    onCreateButtonClick,
    onJoinButtonClick,
    title,
    options,
    createButtonText,
    joinButtonText,
    firstInputLabel,
    secondInputLabel,
    firstInputHint,
    secondInputHint,
  } = props;

  const [firstInputValue, setFirstInputValue] = React.useState('');
  const [secondInputValue, setSecondInputValue] = React.useState('');

  const handleClose = () => {
    onClose(selectedValue);
  };

  const handleFirstInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    setFirstInputValue(value);
    onNameChange(value);
  };

  const handleSecondInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    setSecondInputValue(value);
    onNameChange(value);
  };
  

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>{title}</DialogTitle>
      <List sx={{ pt: 0 }}>
        <ListItem disableGutters>
          <ListItemText primary={firstInputLabel} />
          <div style={{ display: 'flex', alignItems: 'center' }}>
            <input type="text" value={firstInputValue} onChange={handleFirstInputChange} placeholder={firstInputHint} style={{ width: '100%' }} />
            <div className='createButtonDiv'>
              <Button onClick={onCreateButtonClick}>{createButtonText}</Button>
            </div>
          </div>
        </ListItem>
        <ListItem disableGutters>
          <ListItemText primary={secondInputLabel} />
          <div style={{ display: 'flex', alignItems: 'center' }}>
            <input type="text" value={secondInputValue} onChange={handleSecondInputChange} placeholder={secondInputHint} style={{ width: '100%' }} />
            <div className='joinButtonDiv'>
            <Button onClick={onJoinButtonClick}>{joinButtonText}</Button>

            </div>
          </div>
        </ListItem>
      </List>
    </Dialog>
  );
}

export default SimpleDialog;
