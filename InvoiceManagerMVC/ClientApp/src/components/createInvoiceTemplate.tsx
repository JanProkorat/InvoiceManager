import React, {useState} from "react";

interface ICreateInvoiceTemplateProps{
    onAddInvoiceClicked: (invoiceName: string) => void;
}

export const CreateInvoiceTemplate = (props: ICreateInvoiceTemplateProps) => {

    const [invoiceName, setInvoiceName] = useState<string>("");

    return (
        <div className="w-100">
            <h4>Vytvoření faktury</h4>
            <div className="d-flex justify-content-between">
                <label>Název faktury</label>
                <input type="text" value={invoiceName} onChange={e => setInvoiceName(e.target.value)}/>
                <button
                    type="button" onClick={() => props.onAddInvoiceClicked(invoiceName)} disabled={invoiceName === ""}
                >
                    Přidat
                </button>
            </div>
        </div>
    )
}