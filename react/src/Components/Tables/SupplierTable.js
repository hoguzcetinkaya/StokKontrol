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
import Form from '../Forms/CreateSupplierForm.js';
import UpdateSupplierForm from '../Forms/UpdateSupplierForm.js';
import { Button } from '@mui/material';

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

function createData(id,name, phoneNumber, address, city,country) {
  return { id, name, phoneNumber, address, city ,country};
}


export default function SupplierTable() {
  const [rows, setRows] = useState([]);
  const [openUpdateDialog, setOpenUpdateDialog] = useState(false);
  const [currentSupplier, setCurrentSupplier] = useState(null); // Güncellenecek tedarikçinin verileri


  useEffect(() => {
    axios.get('https://localhost:7291/Supplier/GetAll')
      .then(response => {
        const newRows = response.data.map(item => createData(item.id, item.name,item.phoneNumber,item.address,item.city,item.country));
        setRows(newRows);
        console.log(response.data,"hyfryhfrtyh");
      })
      .catch(error => {
        console.log(error);
      });
  }, []);
  


  const handleUpdateClick = (supplier) => {
    setCurrentSupplier(supplier);
    setOpenUpdateDialog(true);
  };

  const handleCloseUpdateDialog = () => {
    setOpenUpdateDialog(false);
    setCurrentSupplier(null);
  };

  const handleUpdateSupplier = (updatedSupplier) => {
    console.log(updatedSupplier,"updateeesupplii")
    // Güncelleme işlemi burada gerçekleşecek
    setOpenUpdateDialog(false);
    setRows(prevRows => prevRows.map(row => 
      row.id === updatedSupplier.id ? { ...row, ...updatedSupplier } : row
    ));
  };
  const handleNewSupplier = (newSupplier) => {
    setRows(prevRows => [...prevRows, createData(newSupplier.name,newSupplier.phoneNumber,newSupplier.address,newSupplier.city,newSupplier.country)]);
  };

  const handleDelete = (itemId) => {
    console.log(itemId,"itemid")
    axios.delete(`https://localhost:7291/Supplier/Delete/${itemId}`)
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
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 1000 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Name</StyledTableCell>
            <StyledTableCell>Phone Number</StyledTableCell>
            <StyledTableCell>Address</StyledTableCell>
            <StyledTableCell>City</StyledTableCell>
            <StyledTableCell>Country</StyledTableCell>
            <StyledTableCell></StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
        {rows.map((row) => (
          <StyledTableRow key={row.id}>
            <StyledTableCell component="th" scope="row">
                {row.name}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.phoneNumber}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.address}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.city}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.country}
              </StyledTableCell>
            <StyledTableCell>
              <Button variant="contained" color="secondary" onClick={() => handleDelete(row.id)}>
                Sil
              </Button>
              <Button variant="contained" color="primary" onClick={() => handleUpdateClick(row)}>
                Güncelle
              </Button>
            </StyledTableCell>
          </StyledTableRow>
        ))}
      </TableBody>
      <Form onNewSupplier={handleNewSupplier} />
      {openUpdateDialog && (
        <UpdateSupplierForm
        open={openUpdateDialog}
        onClose={handleCloseUpdateDialog}
        supplierData={currentSupplier}
        onUpdateSupplier={handleUpdateSupplier} // Bu prop'u ekleyin
      />
      )}
      </Table>
    </TableContainer>
  );
}
