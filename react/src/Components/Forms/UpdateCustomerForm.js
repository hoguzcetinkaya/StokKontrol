import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';

import { TextField } from '@mui/material';
import axios from 'axios';
import React, { useState, useEffect } from 'react';

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));

// Diğer importlar...
export default function UpdateCustomerForm({ open, onClose, customerData, onUpdateCustomer }) {
  const [formData, setFormData] = useState({
    id: '',
    name: '',
    email: '',
    phoneNumber: '',
    address: '',
    city: '',
    country: ''
  });

  useEffect(() => {
    if (customerData) {
        console.log(customerData,"Customer Data")
      setFormData({
        id: customerData.id,
        name: customerData.name || '',
        email: customerData.email || '',
        phoneNumber: customerData.phoneNumber || '',
        address: customerData.address || '',
        city: customerData.city || '',
        country: customerData.country || ''
      });
    }
  }, [customerData]);

  const handleUpdate = () => {
    axios.put('https://localhost:7012/Customer/Update', formData)
      .then(response => {
        onUpdateCustomer(response.data);
        onClose();
      })
      .catch(error => {
        console.error('Error updating supplier:', error);
      });
  };

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData(prevFormData => ({
      ...prevFormData,
      [name]: value
    }));
  };

  return (
    <React.Fragment>
      <BootstrapDialog
        onClose={onClose}
        aria-labelledby="customized-dialog-title"
        open={open}
      >
        <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
          Update Supplier
        </DialogTitle>
        <IconButton aria-label="close" onClick={onClose} sx={{ position: 'absolute', right: 8, top: 8, color: (theme) => theme.palette.grey[500] }}>
          <CloseIcon />
        </IconButton>
        <DialogContent dividers>
          <TextField
            name="name"
            label="Name"
            variant="outlined"
            value={formData.name}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="email"
            label="Email"
            variant="outlined"
            value={formData.email}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="phoneNumber"
            label="Phone Number"
            variant="outlined"
            value={formData.phoneNumber}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="address"
            label="Address"
            variant="outlined"
            value={formData.address}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="city"
            label="City"
            variant="outlined"
            value={formData.city}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="country"
            label="Country"
            variant="outlined"
            value={formData.country}
            onChange={handleChange}
          /><br /><br />
        </DialogContent>
        <DialogActions>
          <Button autoFocus onClick={handleUpdate}>
            Update
          </Button>
        </DialogActions>
      </BootstrapDialog>
    </React.Fragment>
  );
}


