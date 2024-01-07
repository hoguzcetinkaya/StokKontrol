import * as React from 'react';
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import Typography from '@mui/material/Typography';
import { TextField } from '@mui/material';
import axios from 'axios';

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));

export default function Form({ onNewCategory }) {
  const [open, setOpen] = React.useState(false);
  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
    createData();
  };

    const createData = () => {
      const name = document.getElementById("categoryName").value;
      axios.post('https://localhost:7053/Category/Create', {
        name: name
      })
      .then(response => {
        onNewCategory(response.data.name); 
        setOpen(false);
      })
      .catch(error => {
        console.log(error);
      });
    };

  return (
    <React.Fragment>
      <Button variant="outlined" onClick={handleClickOpen}>
        Kategori Oluştur
      </Button>
      <BootstrapDialog
        onClose={handleClose}
        aria-labelledby="customized-dialog-title"
        open={open}
      >
        <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
         sadfas
        </DialogTitle>
        <IconButton
          aria-label="close"
          onClick={handleClose}
          sx={{
            position: 'absolute',
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
        <DialogContent dividers>
        <TextField id="categoryName" label="Category Name" variant="outlined"/><br/>
        </DialogContent>
        <DialogActions>
          <Button autoFocus onClick={handleClose}>
            Create
          </Button>
        </DialogActions>
      </BootstrapDialog>
    </React.Fragment>
  );
}
