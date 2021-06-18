Service DevicesApi implements 6 operations:

1. Add device

Implemented as a POST http-request with a signature {api-url}/devices/add. Information about a device must be transfered in a body of the request in JSON-format.

Example of a body:

{
    "Name": "Phone",
    "Brand": "Sony Ericsson"
}

2. Get device by identifier

Implemented as a GET http-request with a signature {api-url}/devices/getbyid/{id}.

3. List all devices

Implemented as a GET http-request with a signature {api-url}/devices/getall.

4. Update device (full and partial)

Implemented as a PUT http-request with a signature {api-url}/devices/update. Information about a device must be transfered in a body of the request in JSON-format.

Example of a body:

{
    "Brand": "Alcatel"
}

5. Delete a device

Implemented as a DELETE http-request with a signature {api-url}/delete/{id}.

6. Search device by brand

Implemented as a GET http-request with a signature {api-url}/search/{brand}.


All data is stored in local MS SQL Database (software must be installed in advance).

REST service should be hosted in IIS.