import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { styled } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Form from '../Forms/CreateProductForm.js';
import { Button } from '@mui/material';
import UpdateProductForm from '../Forms/UpdateProductForm.js';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));

function createData(id,name, description, price, stockQuantity, supplierName, categoryName) {
  return { id, name, description, price, stockQuantity , supplierName, categoryName};
}

export default function ProductTable() {
  const [rows, setRows] = useState([]);
  const [openUpdateDialog, setOpenUpdateDialog] = useState(false);
  const [currentProduct, setCurrentProduct] = useState(null); // Güncellenecek tedarikçinin verileri

  useEffect(() => {
    axios.get('https://localhost:7188/Product/GetAll')
      .then(response => {
        const newRows = response.data.map(item => createData(item.id, item.name,item.description,item.price,item.stockQuantity,item.supplierName,item.categoryName));
        setRows(newRows);
        console.log(response.data,"hyfryhfrtyh");
      })
      .catch(error => {
        console.log(error);
      });
  }, []);
  
  const handleUpdateClick = (product) => {
    setCurrentProduct(product);
    setOpenUpdateDialog(true);
  };

  const handleCloseUpdateDialog = () => {
    setOpenUpdateDialog(false);
    setCurrentProduct(null);
  };

  const handleUpdateProduct = (updatedProduct) => {
    console.log(updatedProduct, "güncelleneneeeee")
    // Güncelleme işlemi burada gerçekleşecek
    setOpenUpdateDialog(false);
    setRows(prevRows => prevRows.map(row => 
      row.id === updatedProduct.id ? { ...row, ...updatedProduct } : row
    ));
  };

  const handleNewProduct = (newProduct) => {
    setRows(prevRows => [...prevRows, createData(newProduct.id, newProduct.name,newProduct.description,newProduct.price,newProduct.stockQuantity,newProduct.supplierName,newProduct.categoryName)]);
  };

  const handleDelete = (itemId) => {
    console.log(itemId,"itemid")
    axios.delete(`https://localhost:7188/Product/Delete/${itemId}`)
    .then(response => {
      console.log("Silme isteği yanıtı:", response);
      if (response.status === 200) {
        // Silme işlemi başarılıysa, UI'ı güncelle
        setRows(prevRows => prevRows.filter(row => row.id !== itemId));
      }
      })
      .catch(error => {
        // Hata işleme
        console.error('Error deleting item:', error);
      });
  };

  console.log(rows,"rows")
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 1000 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Name</StyledTableCell>
            <StyledTableCell>Description</StyledTableCell>
            <StyledTableCell>Price(₺)</StyledTableCell>
            <StyledTableCell>Stock Quantity</StyledTableCell>
            <StyledTableCell>Supplier Name</StyledTableCell>
            <StyledTableCell>Category Name</StyledTableCell>
            <StyledTableCell></StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <StyledTableRow key={row.name}>
              <StyledTableCell component="th" scope="row">
                {row.name}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.description}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.price}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.stockQuantity}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.supplierName}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.categoryName}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
              <Button 
               variant="contained" 
               color="secondary" 
               onClick={() => handleDelete(row.id)} // item.id, silinecek öğenin ID'si
             >
               Sil
             </Button>
             <Button variant="contained" color="primary" onClick={() => handleUpdateClick(row)}>
                Güncelle
              </Button>
              </StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
        <Form onNewProduct={handleNewProduct}/>
        {openUpdateDialog && (
        <UpdateProductForm
        open={openUpdateDialog}
        onClose={handleCloseUpdateDialog}
        productData={currentProduct}
        onUpdateProduct={handleUpdateProduct} // Bu prop'u ekleyin
      />
      )}
      </Table>
    </TableContainer>
  );
}
