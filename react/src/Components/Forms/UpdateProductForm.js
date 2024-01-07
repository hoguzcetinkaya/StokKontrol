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
export default function UpdateProductForm({ open, onClose, productData, onUpdateProduct, categories, suppliers }) {
  const [formData, setFormData] = useState({
    id: '',
    name: '',
    description: '',
    price: '',
    stockQuantity: ''
  });

  useEffect(() => {
    if (productData) {
        console.log(productData,"Customer Data")
      setFormData({
        id: productData.id,
        name: productData.name || '',
        description: productData.description || '',
        price: productData.price || '',
        stockQuantity: productData.stockQuantity || ''
      });
    }
  }, [productData]);

  const handleUpdate = () => {
    console.log(productData,"Customer Data")
    axios.put('https://localhost:7188/Product/Update', formData)
      .then(response => {
        console.log(response.data,"response dataaa")
        onUpdateProduct(response.data);
        onClose();
      })
      .catch(error => {
        console.error('Error updating product:', error);
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
          Update Product
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
            name="description"
            label="Description"
            variant="outlined"
            value={formData.description}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="price"
            label="Price"
            variant="outlined"
            value={formData.price}
            onChange={handleChange}
          /><br /><br />
          <TextField
            name="stockQuantity"
            label="Stock Quantity"
            variant="outlined"
            type='number'
            value={formData.stockQuantity}
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



