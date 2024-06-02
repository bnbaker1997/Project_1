public class LotService //changed repo to SERVICE
{
    LotRepo lotRepo = new();

    public Lot? AddLot(Lot lot)
    {
        var newLot = lotRepo.AddLot(lot);
        return newLot;
    }

// TODO: change to return the Lot object (retrievedLot) instead of a boolean (true/false)
    public bool UpdateLot(Lot retrievedLot, int lotId)
    {
        if (retrievedLot != null)
        {
            lotRepo.UpdateLot(retrievedLot, lotId);
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
            return false;
        }
    }
}
