import * as React from "react";
import {CreateInvoiceTemplate} from "./createInvoiceTemplate";
import {useEffect, useState} from "react";
import {InvoiceListTemplate} from "./invoiceListTemplate";
import {InvoiceDetailTemplate} from "./invoiceDetailTemplate";

export interface IInvoice {
    id: number;
    invoiceName: string;
    state: InvoiceState;
    items: IInvoiceItem[];
}

export interface IInvoiceItem {
    id: number;
    amountToPay: number;
    receiver: string;
    deadline: string;
    kInvoice: number;
}

export enum InvoiceState {
    nezaplaceno, zaplaceno
}

export const InvoiceScene = () => {

    const [invoices, setInvoices] = useState<IInvoice[]>([]);
    const [detailToDisplay, setDetailToDisplay] = useState<IInvoice | null>(null);

    useEffect(() => {
        get().then((response) => {
            response.text().then((text) => {
                if (response.status === 200) {
                    setInvoices([...invoices, ...JSON.parse(text) as IInvoice[]]);
                }
            })
        })
    }, [])

    const handleAddInvoice = async (invoiceName: string) => {
        let invoice = {
            id: 0,
            invoiceName: invoiceName,
            state: InvoiceState.nezaplaceno,
            items: []
        } as IInvoice;
        await post("/add", JSON.stringify(invoice)).then((response) => {
            if (response.status === 200) {
                response.text().then((text) => {
                    setInvoices([...invoices, JSON.parse(text) as IInvoice]);
                })
            }
        })
    }

    const post = async (route: string, body: string) => {
        return await fetch('api/Invoice' + route, {
            method: 'post',
            headers: {'Content-Type': 'application/json'},
            body: body
        });
    }
    const patch = async (invoiceId: number, body: string) => {
        return await fetch('api/Invoice/' + invoiceId, {
            method: 'patch',
            headers: {'Content-Type': 'application/json'},
            body: body
        });
    }

    const get = async () => {
        return await fetch('api/Invoice', {
            method: 'get',
            headers: {'Content-Type': 'application/json'}
        })
    }

    const deleteInvoiceItem = async (itemId: number) => {
        return await fetch('api/Invoice/delete/item/' + itemId, {
            method: 'delete',
            headers: {'Content-Type': 'application/json'}
        })
    }

    const handleAddInvoiceItem = async (amount: number, receiver: string, deadline: string, invoiceId: number) => {
        let invoiceItem = {
            id: 0,
            kInvoice: invoiceId,
            deadline: deadline,
            receiver: receiver,
            amountToPay: amount
        } as IInvoiceItem;
        await post("/add/item", JSON.stringify(invoiceItem)).then((response) => {
            if (response.status === 200) {
                response.text().then((text) => {
                    let item = JSON.parse(text) as IInvoiceItem;
                    let invoice = {...detailToDisplay} as IInvoice;
                    invoice!.items.push(item);
                    setDetailToDisplay(invoice);
                });
            }
        })
    }

    const handleDeleteInvoiceItem = async (invoiceItemId: number) => {
        await deleteInvoiceItem(invoiceItemId).then((response) => {
            if (response.status === 200) {
                let invoice = {...detailToDisplay} as IInvoice;
                invoice!.items = invoice!.items.filter(item => item.id !== invoiceItemId);
                setDetailToDisplay(invoice);
            }
        })
    }

    const handleUpdateInvoice = async (invoiceId: number, invoiceName: string) => {
        await patch(invoiceId, JSON.stringify({InvoiceName: invoiceName})).then((response) =>{
            if (response.status === 200) {
                response.text().then((text) => {
                    let invoice = JSON.parse(text) as IInvoice;
                    setInvoices(invoices.map((item) =>{
                        if(item.id === invoice.id){
                            return invoice;
                        }
                        return item;
                    }));
                    setDetailToDisplay(invoice);
                });
            }
        })
    }

    const handlePayInvoice = async (invoiceId: number) =>{
        await post("/" + invoiceId, "").then((response) =>{
            if(response.status === 200){
                setInvoices(invoices.filter(item => item.id !== invoiceId));
                setDetailToDisplay(null);
            }
        })
    }

    return (
        <div className="w-100 pt-2 d-flex">
            <div className="col-6 border-right">
                <CreateInvoiceTemplate onAddInvoiceClicked={handleAddInvoice}/>
                <InvoiceListTemplate invoices={invoices} onDetailClick={item => setDetailToDisplay(item)}/>
            </div>
            <div className="col-6 ">
                <InvoiceDetailTemplate
                    detail={detailToDisplay} onAddInvoiceItemClick={handleAddInvoiceItem}
                    onDeleteInvoiceItemClicked={handleDeleteInvoiceItem} onUpdateInvoiceClicked={handleUpdateInvoice}
                    onPayInvoiceClicked={handlePayInvoice}
                />
            </div>
        </div>
    )
}