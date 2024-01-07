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
import Form from '../Forms/CreateCategoryForm';
import { Button } from '@mui/material';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  // ... stil tanımlamaları aynı kalır ...
}));
const StyledTableRow = styled(TableRow)(({ theme }) => ({
  // ... stil tanımlamaları aynı kalır ...
}));

function createData(id, name) {
  return { id, name };
}

export default function KategoriTable() {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7053/Category/GetAll')
      .then(response => {
        const newRows = response.data.map(item => createData(item.id,item.name));
        setRows(newRows);
        console.log(response.data,"hyfryhfrtyh");
      })
      .catch(error => {
        console.log(error);
      });
  }, []);
  
  const handleNewCategory = (newCategory) => {
    setRows(prevRows => [...prevRows, createData(newCategory)]);
  };

  const handleDelete = (itemId) => {
    console.log(itemId,"itemid")
    axios.delete(`https://localhost:7053/Category/Delete/${itemId}`)
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
      <Table sx={{ minWidth: 700 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Kategori</StyledTableCell>
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
              <Button 
               variant="contained" 
               color="secondary" 
               onClick={() => handleDelete(row.id)} // item.id, silinecek öğenin ID'si
             >
               Sil
             </Button>
              </StyledTableCell>
            </StyledTableRow>
            
          ))}
        </TableBody>
        <Form onNewCategory={handleNewCategory}/>
      </Table>
    </TableContainer>
  );
}
