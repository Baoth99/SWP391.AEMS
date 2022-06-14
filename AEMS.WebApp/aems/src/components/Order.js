import * as React from 'react';
import {useState, useEffect} from 'react'
import { DataGrid } from '@mui/x-data-grid';
import {useSelector, useDispatch} from 'react-redux'
import {assetFetch} from '../slices/assetSlice'
import MaxWidthDialog from './editAsset';

const columns = [
  { field: 'id', headerName: 'ID', flex: 1},
  { field: 'name', headerName: 'Name', flex: 1},
  { field: 'brand', headerName: 'Brand', flex: 1},
  {
    field: 'desc',
    headerName: 'Desc',
    flex: 1
  },
  {
    field: 'price',
    headerName: 'Price',
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
  return (
    <div style={{ height: 400, width: '100%' }}>
      <DataGrid
        rows={assetItems.items}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        onSelectionModelChange={(ids) => {
          const selectedIDs = new Set(ids);
          const selectedRows = assetItems.items.filter((row) =>
            selectedIDs.has(row.id),
          );
         selectedRows.length === 0 ? setIsOpen(isOpen) : setIsOpen(!isOpen)
          setSelectedRows(selectedRows);
        }}
      />
      <MaxWidthDialog isDialogOpened={isOpen} items={selectedRows} handleCloseDialog={() => setIsOpen(false)} />
    </div>
  );
}

export default DataTable;
