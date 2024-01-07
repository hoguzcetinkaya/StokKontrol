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
import { MenuItem, TextField } from '@mui/material';
import axios from 'axios';
import { useState } from 'react';
import { useEffect } from 'react';

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));

export default function Form({ onNewProduct }) {
    const [open, setOpen] = React.useState(false);
    const [categories, setCategories] = useState([]);
    const [suppliers, setSuppliers] = useState([]);
    const [supplierId, setSupplierId] = useState('');
    const [categoryId, setCategoryId] = useState('');
    useEffect(() => {
      // Kategorileri ve tedarikçileri çekmek için API çağrıları
      axios.get('https://localhost:7053/Category/GetAll')
        .then(response => {
            console.log(response.data,"categories")
          // Her kategori için sadece isimlerini al
            const categoryNames = response.data.map(category => ({
              id: category.id,
              name: category.name
            }));
            console.log(categoryNames)
            setCategories(categoryNames);
        })
        .catch(error => console.log(error));
  
        axios.get('https://localhost:7291/Supplier/GetAll')
        .then(response => {
            console.log(response.data,"supplier")
          // Her kategori için sadece isimlerini al
            const supplierNames = response.data.map(supplier => ({
                id: supplier.id,
                name: supplier.name
            }));
            console.log(supplierNames)
            setSuppliers(supplierNames);
        })
        .catch(error => console.log(error));
    }, []);
  
    const handleClickOpen = () => {
      setOpen(true);
    };
    const handleClose = () => {
      createData();
      setOpen(false);
    };

    const createData = () => {
      const name = document.getElementById("name").value;
      const description = document.getElementById("description").value;
      const price = document.getElementById("price").value;
      const stockQuantity = document.getElementById("stockQuantity").value;
    //   const supplierId = document.getElementById("supplierId").value;
    //   const categoryId = document.getElementById("categoryId").value;
      
      console.log({ name, description, price, stockQuantity, supplierId, categoryId },"test"); // Kontrol için

      axios.post('https://localhost:7188/Product/Create', {
        name: name,
        description : description,
        price : price,
        stockQuantity : stockQuantity,
        supplierId: parseInt(supplierId), // String'i int'e çevir
        categoryId: parseInt(categoryId), // String'i int'e çevir
      })
      .then(response => {
        console.log(response.data,"aaaaaaaaa")
        onNewProduct(response.data);
        setOpen(false);
      })
      .catch(error => {
        console.log(error);
      });
    };

  return (
    <React.Fragment>
      <Button variant="outlined" onClick={handleClickOpen}>
        Create
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
        <TextField id="name" label="Name" variant="outlined"/><br/>
        <TextField id="description" label="Description" variant="outlined"/><br/>
        <TextField id="price" label="Price" variant="outlined" type='number'/><br/>
        <TextField id="stockQuantity" label="Stock Quantity" variant="outlined" type='number'/><br/>
        <TextField
  id="supplierId"
  select
  label="Supplier"
  variant="outlined"
  value={supplierId}
  onChange={(e) => setSupplierId(e.target.value)}
>
  {suppliers.map((supplier) => (
    <MenuItem key={supplier.id} value={supplier.id}>
      {supplier.name}
    </MenuItem>
  ))}
</TextField>
<br/>
<TextField
  id="categoryId"
  select
  label="Category"
  variant="outlined"
  value={categoryId}
  onChange={(e) => setCategoryId(e.target.value)}
>
  {categories.map((category) => (
    <MenuItem key={category.id} value={category.id}>
      {category.name}
    </MenuItem>
  ))}
</TextField>
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
    
