class LotService //change repo to SERVICE
{
    LotRepo lotRepo = new();

    public void AddLot(Lot lot)
    {
        lotRepo.AddLot(lot);
    }

    public bool UpdateLot(Lot retrievedLot)
    {
        if (retrievedLot != null)
        {
            lotRepo.UpdateLot(retrievedLot);
            return true;
        }
        return false;
    }


    public Lot? GetLot(int? lotId)
    {
        var existingLot = lotRepo.GetLot(lotId);
        return existingLot;
    }
    public List<Lot> GetAvailableLots()
    {
        return lotRepo.GetAvailableLots();
    }

    public List<Lot> GetSoldLots()
    {
        return lotRepo.GetSoldLots();

    }

    public List<Lot> GetAllLots()
    {
        return lotRepo.RetrieveAllLots();
    }

    public bool RemoveLot(Lot retrievedLot)
    {
        // //If we have the ID -> then simply Remove it from storage
        bool didRemove = lotRepo.RemoveLot(retrievedLot.LotId);

        if (didRemove)
        {
            return true;
        }
        else
        {
            System.Console.WriteLine("Not found - enter a valid Lot Number");
            return false;
        }
    }
}
