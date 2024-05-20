class LotRepo
{
    LotStorage lotStorage = new();

    public void AddLot(Lot lot)
    {
        lotStorage.AddLot(lot);
    }

    public bool UpdateLot(Lot retrievedLot)
    {
        if (retrievedLot != null)
        {
            lotStorage.UpdateLot(retrievedLot);
            return true;
        }
        return false;
    }


    public Lot? GetLot(int? lotId)
    {
        var existingLot = lotStorage.GetLot(lotId);
        return existingLot;
    }
    public List<Lot> GetAvailableLots()
    {
        return lotStorage.GetAvailableLots();
    }

    public List<Lot> GetSoldLots()
    {
        return lotStorage.GetSoldLots();

    }

    public List<Lot> GetAllLots()
    {
        return lotStorage.RetrieveAllLots();
    }

    public bool RemoveLot(Lot retrievedLot)
    {
        // //If we have the ID -> then simply Remove it from storage
        bool didRemove = lotStorage.RemoveLot(retrievedLot.LotId);

        if (didRemove)
        {
            return l;
        }
        else
        {
            System.Console.WriteLine("Not found - enter a valid Lot Number");
            return null;
        }
    }
}
