import * as React from 'react';
import {useState, useEffect} from 'react'
import { DataGrid } from '@mui/x-data-grid';
import {useSelector, useDispatch} from 'react-redux'
import {assetFetch} from '../slices/assetSlice'
import MaxWidthDialog from './editAsset';

const columns = [
  { field: 'id', headerName: 'ID', flex: 1},
  { field: 'name', headerName: 'Name', flex: 1},
  { field: 'email', headerName: 'Email', flex: 1},
  {
    field: 'phone',
    headerName: 'Phone',
    flex: 1
  },
  {
    field: 'website',
    headerName: 'Website',
    flex: 1,
  },
];

const DataTable = ()  => {
  const dispatch = useDispatch();
  const assetItems = useSelector((state) => ({...state.asset}));
  const [isOpen, setIsOpen] = useState(false);
  const [selectedRows, setSelectedRows] = React.useState([]);

  useEffect(() => {
     dispatch(assetFetch)
  })

  const selectedModelChange = ids => {
          const selectedIDs = new Set(ids);
          const selectedRows = assetItems.items.filter((row) =>
            selectedIDs.has(row.id),
          );
         selectedRows.length === 1 ? setIsOpen(!isOpen) : setIsOpen(isOpen) 
         setSelectedRows(selectedRows);
        console.log(selectedRows)
  }
  return (
    <div style={{ height: 400, width: '100%' }}>
      <DataGrid
        rows={assetItems.items}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        onSelectionModelChange={(ids) => {selectedModelChange(ids)}}
      />
      <MaxWidthDialog isDialogOpened={isOpen} items={selectedRows[selectedRows.length - 1]} handleCloseDialog={() => setIsOpen(false)} />
    </div>
  );
}

export default DataTable;
